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

        /// <summary>
        /// Encrypts the string data based on the companyCode secret key.
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        string Encrypt(string inputData, string companyCode);

        /// <summary>
        /// Decrypts the encrypted data based on the companyCode secret key.
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        string Decrypt(string encryptedData, string companyCode);
    }
}
