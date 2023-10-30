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
        Task<IDocumentUserEntity> GetDocumentUser(string userName, CancellationToken cancellationToken);

        /// <summary>
        /// Validates the user data for uniqueness of user name, and required fields, and save user entity.
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Unique User ID.</returns>
        Task<string> SaveDocumentUser(IDocumentUserEntity userEntity, CancellationToken cancellationToken);
    }
}
