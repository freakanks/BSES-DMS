using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Data.Contracts;
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
        /// Readonly instance of Data Access respnsible for saving or retreivng the document from physical disk.
        /// </summary>
        private readonly IDocumentDA _documentDA;

        /// <summary>
        /// Readonly instance of Data Access responsible for saving or retreiving the document data from Database.
        /// </summary>
        private readonly IDocumentEntityDA _documentEntityDA;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        public DocumentManagementBA(ILogger<DocumentManagementBA> logger, IDocumentDA documentDA, IDocumentEntityDA documentEntityDA)
        {
            _logger = logger;
            _documentDA = documentDA;
            _documentEntityDA = documentEntityDA;
        }

        ///<inheritdoc/>
        public async Task<Result<(IDocumentEntity, Stream)>> GetDocumentAsync(string documentID, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _documentEntityDA.GetDocumentAsync(documentID, cancellationToken);
                if (entity == null)
                    return new Result<(IDocumentEntity, Stream)>(ValueTuple.Create<IDocumentEntity, Stream>(default, default),
                        false, $"Something went wrong while fetching the document entity for the document id {documentID}");

                var stream = await _documentDA.GetDocumentAsync(entity.DocumentPath, cancellationToken);

                if (stream == null)
                    return new Result<(IDocumentEntity, Stream)>(ValueTuple.Create<IDocumentEntity, Stream>(default, default),
                        false, $"Something went wrong while fetching the document from the document path {entity.DocumentPath}");

                return new Result<(IDocumentEntity, Stream)>(ValueTuple.Create(entity!, stream), true, string.Empty);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
            }
            return new Result<(IDocumentEntity, Stream)>(ValueTuple.Create<IDocumentEntity, Stream>(default, default),
                        false, $"Something went wrong while fetching the document with document id {documentID}");
        }

        ///<inheritdoc/>
        public async Task<Result<string>> SaveDocumentAsync(IDocumentEntity documentEntity, Stream documentStream, CancellationToken cancellationToken)
        {
            try
            {
                documentEntity.DocumentPath = await _documentDA.SaveDocumentAsync(documentEntity.DocumentPath, documentStream, cancellationToken);

                var entity = await _documentEntityDA.SaveDocumentAsync(documentEntity, cancellationToken);
                if (entity == null)
                    return new Result<string>(string.Empty, false, $"Something went wrong while saving the document entity record.");

                return new Result<string>(documentEntity.DocumentID, true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }

            return new Result<string>(string.Empty, false, $"Something went wrong while saving the document.");
        }
    }
}
