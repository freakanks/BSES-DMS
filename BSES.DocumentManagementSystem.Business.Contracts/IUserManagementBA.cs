using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Business.Contracts
{
    /// <summary>
    /// Business Layer for the User Management.
    /// </summary>
    public interface IUserManagementBA
    {
        /// <summary>
        /// Gets the user details for the specified user name.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User Details.</returns>
        Task<IDocumentUserEntity?> GetDocumentUserAsync(string userName, CancellationToken cancellationToken);

        /// <summary>
        /// Validates the user data for uniqueness of user name, and required fields, and save user entity.
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Unique User ID.</returns>
        Task<IDocumentUserEntity?> SaveDocumentUserAsync(IDocumentUserEntity userEntity, CancellationToken cancellationToken);

        /// <summary>
        /// Authenticates the user credentials.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="secretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IDocumentUserEntity?> AuthenticateDocumentUserAsync(string companyCode, string userName, string secretKey, CancellationToken cancellationToken);
    }
}
