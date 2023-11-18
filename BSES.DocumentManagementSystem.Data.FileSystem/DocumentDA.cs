using BSES.DocumentManagementSystem.Data.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace BSES.DocumentManagementSystem.Data.FileSystem
{
    /// <inheritdoc/>
    public class DocumentDA : IDocumentDA
    {
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
        /// Default Constructor.
        /// </summary>
        /// <param name="logger"></param>
        public DocumentDA(ILogger<DocumentDA> logger, IConfiguration configuration)
        {
            _logger = logger;
            _basePathForStorage = configuration.GetRequiredSection("BasePathForStorage").Value ?? throw new Exception($"Base Path for storage is not defined in configuration.");
            _retrialCount = int.TryParse(configuration.GetRequiredSection("RetryCount").Value, out int value) ? value : 1;
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

                    using var fileStream = new FileStream(documentPath, FileMode.Open, FileAccess.Read);
                    using var stream = new MemoryStream(new byte[fileStream.Length]);
                    await fileStream.CopyToAsync(stream);

                    return stream;
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
                    string filepath = Path.Combine(_basePathForStorage, documentName);
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
    }
   
}
