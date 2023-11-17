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
    }
}
