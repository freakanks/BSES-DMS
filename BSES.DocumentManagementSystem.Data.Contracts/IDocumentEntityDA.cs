using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Data.Contracts
{
    /// <summary>
    /// Document Entity Databse operations.
    /// </summary>
    public interface IDocumentEntityDA
    {
        /// <summary>
        /// Asynchronously gets the document information for the documentID.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Document information with the latest version.</returns>
        Task<IDocumentEntity?> GetDocumentAsync(string documentID, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchrobously saves the document information to the databse.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Saved document information.</returns>
        Task<IDocumentEntity> SaveDocumentAsync(IDocumentEntity document, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronusly saves all the documents provided.
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Collection saved document information.</returns>
        Task<IEnumerable<IDocumentEntity>> SaveAllDocumentsAsync(IEnumerable<IDocumentEntity> documents, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously removes/soft deletes the document information.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Success/Failure</returns>
        Task<IDocumentEntity> RemoveDocumentAsync(string documentID, string userID, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously updates the document information by creating a new record with new version.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="newDocument"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>New document information with newer version.</returns>
        Task<IDocumentEntity> UpdateDocumentAsync(string documentID, IDocumentEntity newDocument, CancellationToken cancellationToken);

        /// <summary>
        /// Asyncronously marks the document as archived or unarchived, by setting the flag IsArchived true.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> MarkDocumentArchivedOrUnArchivedAsync(string documentID, string newPath, bool archived, CancellationToken cancellationToken);
    }
}
