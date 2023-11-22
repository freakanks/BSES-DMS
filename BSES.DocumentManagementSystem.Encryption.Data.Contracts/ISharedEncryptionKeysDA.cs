namespace BSES.DocumentManagementSystem.Encryption.Data.Contracts
{
    /// <summary>
    /// Provides data access for the encryptions keys being used by DMS application.
    /// </summary>
    public interface ISharedEncryptionKeysDA
    {
        /// <summary>
        /// Gets all the encryption keys being used for DMS application.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IDictionary<string, string>> GetAllEncryptionKeysAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets all the encryption IV keys being used for DMS application.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IDictionary<string, string>> GetAllEncryptionIVKeysAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the encryption keys being used for DMS application for particular company code.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetEncryptionKeysAsync(string companyCode, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the encryption IV keys being used for DMS application for particular company code.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetEncryptionIVKeysAsync(string companyCode, CancellationToken cancellationToken);

    }
}
