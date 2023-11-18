using BSES.DocumentManagementSystem.Data.Contracts;
using BSES.DocumentManagementSystem.Entities.Contracts;
using BSES.DocumentManagementSystem.Entities.Contracts.Document;
using BSES.DocumentManagementSystem.Entities.Document;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSES.DocumentManagementSystem.Data
{
    ///<inheritdoc/>
    public class DocumentLogEntityDA : IDocumentLogEntityDA
    {
        /// <summary>
        /// Local readonly instance of context to perform DB operations.
        /// </summary>
        private readonly DMSDBContext _context;

        /// <summary>
        /// Converts the db model to entity.
        /// </summary>
        /// <param name="accessLog"></param>
        /// <returns></returns>
        private IDocumentLogEntity ModelToLogEntity(AccessLog accessLog) =>
            new DocumentLogEntity()
            {
                ActionTaken = Enum.TryParse($"{accessLog.ActionTaken}", out DocumentAction action) ? action : DocumentAction.Read,
                DocumentID = accessLog.DocumentID,
                LogId = accessLog.ID,
                UserId = accessLog.UserId,

                CreatedDateTime = accessLog.CreatedDateTime,
                UpdatedDateTime = accessLog.UpdatedDateTime,
                CreatedUserID = accessLog.CreatedUserID,
                UpdatedUserID = accessLog.UpdatedUserID,
                RecordStatusCode = Enum.TryParse($"{accessLog.RecordStatusCode}", out RecordStatusCode statusCode) ? statusCode : RecordStatusCode.Active
            };

        /// <summary>
        /// Converts the entity to DB model.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private AccessLog EntityToAccessLog(IDocumentLogEntity accessLog) =>
            new AccessLog()
            {
                ActionTaken = (int)accessLog.ActionTaken,
                DocumentID = accessLog.DocumentID,
                UserId = accessLog.UserId,

                CreatedDateTime = accessLog.CreatedDateTime,
                UpdatedDateTime = accessLog.UpdatedDateTime,
                CreatedUserID = accessLog.CreatedUserID,
                UpdatedUserID = accessLog.UpdatedUserID,
                RecordStatusCode = (int)accessLog.RecordStatusCode
            };

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="context"></param>
        public DocumentLogEntityDA(DMSDBContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<IDocumentLogEntity>> GetDocumentLogsAsync(string documentID, CancellationToken cancellationToken)
        {
            var logs = await _context.AccessLogs.Where(x => x.DocumentID == documentID).ToListAsync(cancellationToken);
            return logs == null ? new List<IDocumentLogEntity>() : logs.Select(x => ModelToLogEntity(x));
        }

        public async Task<IEnumerable<IDocumentLogEntity>> SaveAllDocumentsLogAsync(string documentID, IEnumerable<IDocumentLogEntity> logs, CancellationToken cancellationToken)
        {
            if (logs?.Any() ?? false)
            {
                var result = new List<IDocumentLogEntity>();
                foreach (var log in logs)
                {
                    var currentLog = await _context.AccessLogs.AddAsync(EntityToAccessLog(log), cancellationToken);
                    result.Add(ModelToLogEntity(currentLog.Entity));
                }
                await _context.SaveChangesAsync(cancellationToken);
                return result;
            }
            return new List<IDocumentLogEntity>();
        }

        public async Task<IDocumentLogEntity> SaveDocumentLogAsync(string documentID, IDocumentLogEntity log, CancellationToken cancellationToken)
        {
            var result = await _context.AccessLogs.AddAsync(EntityToAccessLog(log), cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return ModelToLogEntity(result.Entity);
        }

    }
}
