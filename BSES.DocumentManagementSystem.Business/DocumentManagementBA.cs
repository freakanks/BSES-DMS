using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Data.Contracts;
using BSES.DocumentManagementSystem.Entities;
using BSES.DocumentManagementSystem.Entities.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BSES.DocumentManagementSystem.Business
{
    public class DocumentManagementBA : IDocumentManagementBA
    {
        private string ValidateDocumentForArchival(IDocumentEntity documentEntity)
        {
            if (string.IsNullOrEmpty(documentEntity.DocumentID))
                return $"Invalid DocumentID.";
            if (string.IsNullOrEmpty(documentEntity.DocumentPath))
                return $"Invalid DocumentPath";
            if (documentEntity.IsArchived)
                return $"Document is already archived.";
            return string.Empty;
        }
        /// <summary>
        /// Readonly local instance of the current user.
        /// </summary>
        private readonly IDocumentUserEntity? _currentUser;
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

        private async Task<IDocumentEntity?> PerformIfDocumentIsArchivedAsync(IDocumentEntity entity, CancellationToken cancellationToken)
        {
            if (entity.IsArchived)
            {
                string newDocumentPath = await _documentDA.UnArchivedDocumentAsync(entity.DocumentPath, _currentUser!.CompanyCode, cancellationToken);
                if (string.IsNullOrEmpty(newDocumentPath))
                    return null;

                bool unarchiveResult = await _documentEntityDA.MarkDocumentArchivedOrUnArchivedAsync(entity.DocumentID, newDocumentPath, false, cancellationToken);
                if (!unarchiveResult)
                    return null;

                entity.DocumentPath = newDocumentPath;
            }
            return entity;
        }

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
            _currentUser = httpContextAccessor.HttpContext.Session.Get<DocumentUserEntity>(DMSConstants.USER_SESSION_DATA);
        }

        ///<inheritdoc/>
        public async Task<Result<(IDocumentEntity, Stream)>> GetDocumentAsync(string documentID, CancellationToken cancellationToken)
        {
            try
            {
                await _documentLogEntityDA.SaveDocumentLogAsync(documentID, new DocumentLogEntity(documentID, _currentUser?.UserID!, DocumentAction.Read), cancellationToken);

                var entity = await _documentEntityDA.GetDocumentAsync(documentID, cancellationToken);
                if (entity == null)
                    return new Result<(IDocumentEntity, Stream)>(ValueTuple.Create<IDocumentEntity, Stream>(default, default),
                        false, $"Something went wrong while fetching the document entity for the document id {documentID}");

                if (_currentUser == null || !entity.DocumentPath.ToLower().Contains($@"{_currentUser.CompanyCode.ToLower()}"))
                    return new Result<(IDocumentEntity, Stream)>(ValueTuple.Create<IDocumentEntity, Stream>(default, default),
                        false, $"User does not have the access rights for the document id {documentID}");

                entity = await PerformIfDocumentIsArchivedAsync(entity, cancellationToken);

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
        public async Task<Result<bool>> MarkDocumentArchiveAsync(IDocumentEntity documentEntity, string companyCode, CancellationToken cancellationToken)
        {
            try
            {
                string validateMessage = ValidateDocumentForArchival(documentEntity);
                if (!string.IsNullOrEmpty(validateMessage))
                    return new Result<bool>(false, false, validateMessage);

                string newDocumentPath = await _documentDA.ArchiveDocumentAsync(documentEntity.DocumentPath, companyCode, cancellationToken);
                if (string.IsNullOrEmpty(newDocumentPath))
                    return new Result<bool>(false, false, $"Something went worng while archiving the document. Kindly get in touch with administrator.");

                var result = await _documentEntityDA.MarkDocumentArchivedOrUnArchivedAsync(documentEntity.DocumentID, newDocumentPath, true, cancellationToken);
                if (result == false)
                    return new Result<bool>(false, false, $"Something went worng while saving the archiving information of the document. Kindly get in touch with administrator.");

                return new Result<bool>(true, true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
            return new Result<bool>(false, false, $"Something went wrong whil amking the document archive.");
        }

        ///<inheritdoc/>
        public async Task<Result<bool>> RemoveDocumentAsync(string documentID, CancellationToken cancellationToken)
        {
            try
            {
                if (_currentUser == null || _currentUser.UserRight < DocumentUserRight.RemoveAccess)
                    return new Result<bool>(false, false, $"User does not have rights to remove the document {documentID}");

                await _documentLogEntityDA.SaveDocumentLogAsync(documentID, new DocumentLogEntity(documentID, _currentUser?.UserID!, DocumentAction.Delete), cancellationToken);

                var entity = await _documentEntityDA.RemoveDocumentAsync(documentID, _currentUser?.UserName ?? string.Empty , cancellationToken);
                if (entity == null)
                    return new Result<bool>(false, false, $"Something went wrong while removing the document entity for the document id {documentID}");

                var removeResult = _documentDA.RemoveDocumentAsync(entity.DocumentPath, cancellationToken);

                if (!removeResult)
                    return new Result<bool>(false, false, $"Something went wrong while removing the document from the document path {entity.DocumentPath}");

                return new Result<bool>(true, true, string.Empty);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
            }
            return new Result<bool>(false, false, $"Something went wrong while removing the document with document id {documentID}");
        }

        ///<inheritdoc/>
        public async Task<Result<string>> SaveDocumentAsync(IDocumentEntity documentEntity, Stream documentStream, CancellationToken cancellationToken)
        {
            try
            {
                if (_currentUser == null || _currentUser.UserRight < DocumentUserRight.WriteAccess)
                    return new Result<string>(string.Empty, false, $"User does not have rights to add the document {documentEntity.DocumentName}");


                documentEntity.DocumentPath = await _documentDA.SaveDocumentAsync(documentEntity.DocumentName, documentStream, cancellationToken);

                if (string.IsNullOrEmpty(documentEntity.DocumentPath))
                    return new Result<string>(string.Empty, false, $"Something went wrong while saving the document stream for document name {documentEntity.DocumentName}.");

                documentEntity.DocumentID = $"{Guid.NewGuid()}";
                documentEntity.UpdatedUserID = documentEntity.CreatedUserID = _currentUser.UserName;

                var entity = await _documentEntityDA.SaveDocumentAsync(documentEntity, cancellationToken);
                if (entity == null)
                    return new Result<string>(string.Empty, false, $"Something went wrong while saving the document entity record.");

                await _documentLogEntityDA.SaveDocumentLogAsync(documentEntity.DocumentID, new DocumentLogEntity(documentEntity.DocumentID, _currentUser?.UserID!, DocumentAction.Write), cancellationToken);

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
