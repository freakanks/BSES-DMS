using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Data.Contracts;
using BSES.DocumentManagementSystem.Entities;
using BSES.DocumentManagementSystem.Entities.Contracts;
using Microsoft.AspNetCore.Http;
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
        /// Readonly instance of Data Access for access logs for documents.
        /// </summary>
        private readonly IDocumentLogEntityDA _documentLogEntityDA;

        /// <summary>
        /// Current User Accessing the system.
        /// </summary>
        private readonly IDocumentUserEntity? _userEntity;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="documentDA"></param>
        /// <param name="documentEntityDA"></param>
        /// <param name="documentLogEntityDA"></param>
        /// <param name="httpContextAccessor"></param>
        public DocumentManagementBA(ILogger<DocumentManagementBA> logger, IDocumentDA documentDA, IDocumentEntityDA documentEntityDA, IDocumentLogEntityDA documentLogEntityDA, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _documentDA = documentDA;
            _documentEntityDA = documentEntityDA;
            _documentLogEntityDA = documentLogEntityDA;
            _userEntity = httpContextAccessor.HttpContext.Session.Get<IDocumentUserEntity>(DMSConstants.USER_SESSION_DATA);
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

                if (string.IsNullOrEmpty(documentEntity.DocumentPath))
                    return new Result<string>(string.Empty, false, $"Something went wrong while saving the document stream for document name {documentEntity.DocumentName}.");

                documentEntity.DocumentID = $"{Guid.NewGuid()}";

                var entity = await _documentEntityDA.SaveDocumentAsync(documentEntity, cancellationToken);
                if (entity == null)
                    return new Result<string>(string.Empty, false, $"Something went wrong while saving the document entity record.");

                await _documentLogEntityDA.SaveDocumentLogAsync(documentEntity.DocumentID, new DocumentLogEntity(documentEntity.DocumentID, _userEntity.UserID, DocumentAction.Write), cancellationToken);

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
