using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Entities.Contracts;
using Microsoft.Extensions.Logging;

namespace BSES.DocumentManagementSystem.Business
{
    public class DocumentManagementBA : IDocumentManagementBA
    {
        /// <summary>
        /// Readonly instance of logger.
        /// </summary>
        private readonly ILogger<DocumentManagementBA> _logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        public DocumentManagementBA(ILogger<DocumentManagementBA> logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
        public Task<Result<IDocumentEntity>> GetDocumentAsync(string documentID, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public Task<Result<string>> SaveDocumentAsync(Stream documentStream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
