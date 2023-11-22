namespace BSES.DocumentManagementSystem.Business.Contracts
{
    /// <summary>
    /// Singleton class responsible for encryption and decryption of the file stream usign AES 256.
    /// </summary>
    public interface IEncryptorDecryptorBA
    {
        /// <summary>
        /// Encrypts the stream data.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Encrypted Stream data.</returns>
        Task<Stream> EncryptAsync(Stream stream, CancellationToken cancellationToken);

        /// <summary>
        /// Decrypts the stream data.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Decrypted stream data.</returns>
        Task<Stream> DecryptAsync(Stream stream, CancellationToken cancellationToken);

        /// <summary>
        /// Encrypts the string data based on the companyCode secret key.
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="companyCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> EncryptAsync(string inputData, string companyCode, CancellationToken cancellationToken);

        /// <summary>
        /// Decrypts the encrypted data based on the companyCode secret key.
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="companyCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> DecryptAsync(string encryptedData, string companyCode, CancellationToken cancellationToken);
    }
}
