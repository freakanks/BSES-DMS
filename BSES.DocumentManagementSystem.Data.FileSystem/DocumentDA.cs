using BSES.DocumentManagementSystem.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace BSES.DocumentManagementSystem.Data.FileSystem
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class DocumentDA : IDocumentDA
    {
        /// <summary>
        /// Logger for this.
        /// </summary>
        private readonly ILogger<DocumentDA> _logger;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="logger"></param>
        public DocumentDA(ILogger<DocumentDA> logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
        public Task<FileStream> GetDocumentAsync(string documentPath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public Task<bool> RemoveDocumentAsync(string documentPath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public Task<string> SaveDocumentAsync(string documentID, Stream documentStream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
