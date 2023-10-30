using BSES.DocumentManagementSystem.Business.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSES.DocumentManagementSystem.Controllers
{
    [Route("api/DocumentManagement")]
    [ApiController]
    public class DocumentManagementController : ControllerBase
    {
        /// <summary>
        /// Internal Logger for this class instance.
        /// </summary>
        private readonly ILogger<DocumentManagementController> _logger;

        /// <summary>
        /// Local Instance for Document Management Business Services.
        /// </summary>
        private readonly IDocumentManagementBA _documentManagementBA;

        /// <summary>
        /// Default Contructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="documentManagementBA"></param>
        public DocumentManagementController(ILogger<DocumentManagementController> logger, IDocumentManagementBA documentManagementBA)
        {
            _logger = logger;
            _documentManagementBA = documentManagementBA;
        }

        [HttpGet("GetDocument")]
        /// <summary>
        /// Asynchronously returns the document with the unique document ID.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<FileContentResult> GetDocumentAsync(string documentID, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        [HttpPost("SaveDocument")]
        /// <summary>
        /// Asynchronously Saves the document data stream.
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Unique Document ID for the saved document stream.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> SaveDocumentAsync(FileStream dataStream, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }
}
