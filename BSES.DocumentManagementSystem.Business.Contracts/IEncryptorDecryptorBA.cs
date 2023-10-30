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
        /// <returns>Encrypted Stream data.</returns>
        Stream Encrypt(Stream stream);

        /// <summary>
        /// Decrypts the stream data.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>Decrypted stream data.</returns>
        Stream Decrypt(Stream stream);
    }
}
