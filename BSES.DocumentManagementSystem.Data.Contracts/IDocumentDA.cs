namespace BSES.DocumentManagementSystem.Data.Contracts
{
    /// <summary>
    /// Performs operation for getting, saving, removing document from the drive.
    /// </summary>
    public interface IDocumentDA
    {
        /// <summary>
        /// Asynchronously tries to fetch the document from the path provided.
        /// </summary>
        /// <param name="documentPath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>File as a stream.</returns>
        Task<Stream> GetDocumentAsync(string documentPath, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously tries to unarchive document by defaltion and then removed the zipped file.
        /// </summary>
        /// <param name="documentPath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> UnArchivedDocumentAsync(string documentPath,string companyCode, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously saves the document stream with the name provided.
        /// </summary>
        /// <param name="documentName"></param>
        /// <param name="documentStream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Path of stored document.</returns>
        Task<string> SaveDocumentAsync(string documentName, Stream documentStream, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously removes the document from the path specified.
        /// </summary>
        /// <param name="documentPath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Success/Failure.</returns>
        bool RemoveDocumentAsync(string documentPath, CancellationToken cancellationToken);

        /// <summary>
        /// WaterMarks the document with the key for company code and then archives it.
        /// </summary>
        /// <param name="documentPath"></param>
        /// <param name="companyCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The new path where the archived document is stored.</returns>
        Task<string> ArchiveDocumentAsync(string documentPath, string companyCode, CancellationToken cancellationToken);
    }
}
