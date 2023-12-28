using BSES.DocumentManagementSystem.Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Entities;
using System.IO.Compression;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using SkiaSharp;
using System.Runtime.InteropServices;
using PdfSharp.Fonts;

namespace BSES.DocumentManagementSystem.Data.FileSystem
{
    /// <inheritdoc/>
    public class DocumentDA : IDocumentDA
    {
        /// <summary>
        /// Readonly instance of configurations.
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Base path for storing all the documents.
        /// </summary>
        private readonly string _basePathForStorage;

        /// <summary>
        /// Number of retrials to fetch or save the doucment to be initialised from the configuration.
        /// </summary>
        private readonly int _retrialCount;
        /// <summary>
        /// Logger for this.
        /// </summary>
        private readonly ILogger<DocumentDA> _logger;

        /// <summary>
        /// Opens the image/dcoument and creates a new watermarked document 
        /// </summary>
        /// <param name="documentPath"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string AddWaterMark(string documentPath, string archivalFolder, string companyCode)
        {
            string? watermarktext = _configuration.GetRequiredSection($"{DMSConstants.WATER_MARK_TEXT}{companyCode}").Value;
            if (string.IsNullOrEmpty(watermarktext))
                throw new InvalidOperationException($"Water Mark can not be applied as can not find one for {companyCode}");

            string extension = Path.GetExtension(documentPath).ToUpper();
            string documentName = Path.GetFileNameWithoutExtension(documentPath);
            string baseFolder = Path.Combine(archivalFolder, $"{DateTime.Now.Year}", $"{DateTime.Now.Month}");
            Directory.CreateDirectory(baseFolder);
            string outpuPath = Path.Combine(baseFolder, $"{documentName}_watermarked{extension}");

            if (extension == ".PDF")
            {
                var document = PdfReader.Open(documentPath, PdfDocumentOpenMode.Import);
                var newDocument = new PdfDocument();               

                XFont font = new XFont("ArialMT", 38, XFontStyleEx.Bold);

                foreach (PdfPage page in document.Pages)
                {
                    // Clone the page to the output document
                    PdfPage newPage = newDocument.AddPage(page);

                    using (XGraphics gfx = XGraphics.FromPdfPage(newPage, XGraphicsPdfPageOptions.Append))
                    {
                        // Create a semi-transparent watermark
                        XBrush brush = new XSolidBrush(XColor.FromArgb(50, XColor.FromKnownColor(XKnownColor.DarkRed)));
                        gfx.DrawString(watermarktext, font, brush,
                            new XPoint(newPage.Width / 2, newPage.Height / 2),
                            XStringFormats.Center);
                    }
                }

                // Save the output PDF with the watermark
                newDocument.Save(outpuPath);
                return outpuPath;
            }
            else if (extension == ".JPG" || extension == ".JPEG" || extension == ".PNG")
            {
                using (SKBitmap bitmap = SKBitmap.Decode(documentPath))
                using (SKCanvas canvas = new SKCanvas(bitmap))
                using (SKPaint paint = new SKPaint())
                {
                    // Determine text size and position
                    paint.TextSize = 50;
                    paint.StrokeWidth = 4;
                    paint.Style = SKPaintStyle.Fill;
                    paint.StrokeJoin = SKStrokeJoin.Round;
                    paint.Color = new SKColor(242, 44, 61, (byte)(255 * 0.2));

                    SKRect bounds = new SKRect();
                    paint.MeasureText(watermarktext, ref bounds);
                    float x = (bitmap.Width - bounds.Width) / 2;
                    float y = (bitmap.Height + bounds.Height) / 2;

                    // Draw the watermark text on the image
                    canvas.DrawText(watermarktext, x, y, paint);

                    // Save the modified image to the output path
                    using (SKImage image = SKImage.FromBitmap(bitmap))
                    using (SKData data = image.Encode())
                    using (FileStream stream = File.OpenWrite(outpuPath))
                    {
                        data.SaveTo(stream);
                    }
                }
                return outpuPath;
            }
            return string.Empty;
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="logger"></param>
        public DocumentDA(ILogger<DocumentDA> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            ReadOnlySpan<char> companyCode = httpContextAccessor?.HttpContext?.Session?.Get<DocumentUserEntity>(DMSConstants.USER_SESSION_DATA)?.CompanyCode ?? "BRPL";
            _basePathForStorage = configuration.GetRequiredSection($"{DMSConstants.BASE_STORAGE_PATH_KEY}{companyCode}").Value ?? string.Empty;
            _retrialCount = int.TryParse(configuration.GetRequiredSection("RetryCount").Value, out int value) ? value : 1;
        }

        public async Task<string> ArchiveDocumentAsync(string documentPath, string companyCode, CancellationToken cancellationToken)
        {
            try
            {
                string? archivalPath = _configuration.GetRequiredSection($"{DMSConstants.BASE_PATH_FOR_ARCHIVAL_STORAGE}{companyCode}").Value;
                if (string.IsNullOrEmpty(archivalPath))
                    throw new InvalidOperationException($"Archival Path had been not defined for the company code {companyCode}");

                string newDocumentPath = AddWaterMark(documentPath, archivalPath, companyCode);

                using var filestream = new FileStream(newDocumentPath, FileMode.Open, FileAccess.Read);
                string archivedDocumentPath = $"{newDocumentPath}.gz";
                using var newFileStream = new FileStream(archivedDocumentPath, FileMode.CreateNew, FileAccess.Write);
                using var gzipStream = new GZipStream(newFileStream, CompressionLevel.SmallestSize);
                await filestream.CopyToAsync(gzipStream);

                await filestream.DisposeAsync();
                File.Delete(newDocumentPath);
                return archivedDocumentPath;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
            return null;
        }

        ///<inheritdoc/>
        public async Task<Stream> GetDocumentAsync(string documentPath, CancellationToken cancellationToken)
        {
            int trialCount = 0;
            while (trialCount++ < _retrialCount)
            {
                try
                {
                    if (!File.Exists(documentPath))
                        return null;

                    var fileStream = new FileStream(documentPath, FileMode.Open, FileAccess.Read);
                    return fileStream;
                }
                catch (Exception e)
                {
                    _logger.LogError($"{e}");
                }
            }
            return null;
        }

        ///<inheritdoc/>
        public bool RemoveDocumentAsync(string documentPath, CancellationToken cancellationToken)
        {
            int trialCount = 0;
            while (trialCount++ < _retrialCount)
            {
                try
                {
                    if (File.Exists(documentPath))
                        File.Delete(documentPath);
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError($"{e}");
                }
            }
            return false;
        }

        ///<inheritdoc/>
        public async Task<string> SaveDocumentAsync(string documentName, Stream documentStream, CancellationToken cancellationToken)
        {
            int trialCount = 0;
            while (trialCount++ < _retrialCount)
            {
                try
                {
                    if (string.IsNullOrEmpty(_basePathForStorage))
                        throw new InvalidOperationException($"Base path for storage is not defined.");

                    var storageDirectory = Path.Combine(_basePathForStorage, $"{DateTime.Today.Year}", $"{DateTime.Today.Month}");
                    if (!Directory.Exists(storageDirectory))
                        Directory.CreateDirectory(storageDirectory);

                    string filepath = Path.Combine(storageDirectory, $"{DateTime.Now.Ticks}_{documentName}");

                    using var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                    await documentStream.CopyToAsync(fileStream, cancellationToken);

                    return filepath;
                }
                catch (Exception e)
                {
                    _logger.LogError($"{e}");
                }
            }
            return null;
        }

        ///<inheritdoc/>
        public async Task<string> UnArchivedDocumentAsync(string documentPath, string companyCode, CancellationToken cancellationToken)
        {
            string? baseStoragePath = _configuration.GetRequiredSection($"{DMSConstants.BASE_STORAGE_PATH_KEY}{companyCode}").Value;
            if (string.IsNullOrEmpty(baseStoragePath))
                throw new InvalidOperationException($"Base storage Path had been not defined for the company code {companyCode}");
            string originalDocumentpath = Path.Combine(baseStoragePath, $"{DateTime.Now.Year}", $"{DateTime.Now.Month}", Path.GetFileNameWithoutExtension(documentPath).Replace("_watermarked", ""));

            using var fileStream = new FileStream(originalDocumentpath, FileMode.Create, FileAccess.Write);
            using var zippedFile = new FileStream(documentPath, FileMode.Open, FileAccess.Read);
            using var gzipStream = new GZipStream(zippedFile, CompressionMode.Decompress);
            await gzipStream.CopyToAsync(fileStream);
            await gzipStream.DisposeAsync();

            File.Delete(documentPath);

            return originalDocumentpath;
        }
    }

}
