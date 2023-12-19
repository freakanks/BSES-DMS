using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Business.Contracts
{
    /// <summary>
    /// Business Layer for document management.
    /// </summary>
    public interface IDocumentManagementBA
    {
        /// <summary>
        /// Takes in the document Id, searches the db entry of document and fetches the document stream from the path mentioned in entry.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Document Entity.</returns>
        Task<Result<(IDocumentEntity, Stream)>> GetDocumentAsync(string documentID, CancellationToken cancellationToken);

        /// <summary>
        /// Creates an entry of document record, saves the document to the path and returns the unique id for document stored.
        /// </summary>
        /// <param name="documentEntity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Unique ID for the document.</returns>
        Task<Result<string>> SaveDocumentAsync(IDocumentEntity documentEntity, Stream documentStream, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the doucment physically from the drive and mark the database entry for the file as InActive.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<bool>> RemoveDocumentAsync(string documentID, CancellationToken cancellationToken);

        /// <summary>
        /// Watermarks the pdf and images and the archive the document.
        /// Path for new document will be updated to database and doucment will be marked as archived.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="companyCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<bool>> MarkDocumentArchiveAsync(IDocumentEntity documentEntity, string companyCode, CancellationToken cancellationToken);
    }
}
