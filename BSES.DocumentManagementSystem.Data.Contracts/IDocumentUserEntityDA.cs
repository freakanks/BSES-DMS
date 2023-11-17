using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Data.Contracts
{
    /// <summary>
    /// Document User Database Operations.
    /// </summary>
    public interface IDocumentUserEntityDA
    {
        /// <summary>
        /// Asynchronously gets the User for the user id.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IDocumentUserEntity?> GetAsync(string userID, CancellationToken cancellationToken);
        /// <summary>
        /// Asynchrnously saves the user in database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User information saved record.</returns>
        Task<IDocumentUserEntity> SaveAsync(IDocumentUserEntity user, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchrnously updates the user secret key for authentication.
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User information with updates data.</returns>
        Task<IDocumentUserEntity> UpdateAsync(string userID, string secretKey, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously updates the user global rights,
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userRights"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User information with updates data.</returns>
        Task<IDocumentUserEntity> UpdatesAsync(string userID, DocumentUserRight userRights, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously removes the user soft deleting the record.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Success/Failure</returns>
        Task<bool> RemoveAsync(string userID, CancellationToken cancellationToken);
    }
}
