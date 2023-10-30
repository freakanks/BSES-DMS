using BSES.DocumentManagementSystem.Entities.Contracts.Document;

namespace BSES.DocumentManagementSystem.Data.Contracts
{
    /// <summary>
    /// Document Logs Database Operations.
    /// </summary>
    public interface IDocumentLogEntityDA
    {
        /// <summary>
        /// Asynchronously Gets all the document access logs for a document.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The collection of all the logs for document access.</returns>
        Task<IEnumerable<IDocumentLogEntity>> GetDocumentLogsAsync(string documentID, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously creates a new record of access log.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="log"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Saved Log entity.</returns>
        Task<IDocumentLogEntity>  SaveDocumentLogAsync(string documentID, IDocumentLogEntity log, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously saves the collection of the logs sent.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="logs"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Collection of saved logs records.</returns>
        Task<IEnumerable<IDocumentLogEntity>> SaveAllDocumentsLogAsync(string documentID, IEnumerable<IDocumentLogEntity> logs, CancellationToken cancellationToken);
    }
}
