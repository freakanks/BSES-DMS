using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Encryption.Data.Contracts;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace BSES.DocumentManagementSystem.Business
{
    ///<inheritdoc/>
    public class EncryptorDecryptorBA : IEncryptorDecryptorBA
    {
        /// <summary>
        /// Local dictionary to keep hold of all the encryption keys for companies.
        /// </summary>
        private IDictionary<string, string> _encryptionKeys;

        /// <summary>
        /// Local dictionary to keep hold of all the encryption IV for companies.
        /// </summary>
        private IDictionary<string, string> _IV;

        /// <summary>
        /// Readonly instance for logger.
        /// </summary>
        private readonly ILogger<EncryptorDecryptorBA> _logger;

        /// <summary>
        /// Reads the encryption keys for a company code.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        private string ReadEncryptionKey(string companyCode) =>
                    _encryptionKeys.TryGetValue(companyCode, out string? value) && !string.IsNullOrEmpty(value) ? value :
                    throw new KeyNotFoundException($"Encryption Key not found for the Company Code {companyCode}");

        /// <summary>
        /// Reads the encryption key IV for a company code.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private string ReadIVKey(string companyCode) =>
                    _IV.TryGetValue(companyCode, out string? value) && !string.IsNullOrEmpty(value) ? value :
                    throw new KeyNotFoundException($"Encryption Key IV not found for the Company Code {companyCode}");


        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        public EncryptorDecryptorBA(ILogger<EncryptorDecryptorBA> logger, ISharedEncryptionKeysDA encryptionKeysDA)
        {
            _logger = logger;
            _encryptionKeys = encryptionKeysDA.GetAllEncryptionKeysAsync(default).Result;
            _IV = encryptionKeysDA.GetAllEncryptionIVKeysAsync(default).Result;
        }

        public Task<Stream> EncryptAsync(Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DecryptAsync(Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> EncryptAsync(string inputData, string companyCode, CancellationToken cancellationToken)
        {
            byte[] aesKey = Convert.FromBase64String(ReadEncryptionKey(companyCode));
            byte[] aesIV = Convert.FromBase64String(ReadIVKey(companyCode));

            using var aesManaged = new AesManaged()
            {
                BlockSize = 128,
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                Key = aesKey,
                IV = aesIV
            };
            ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesKey, aesIV);
            using var ms = new MemoryStream();
            using var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            await cryptoStream.WriteAsync(Encoding.UTF8.GetBytes(inputData), cancellationToken);

            using var streamReader = new StreamReader(ms);
            return await streamReader.ReadToEndAsync();
        }

        public async Task<string> DecryptAsync(string encryptedData, string companyCode, CancellationToken cancellationToken)
        {
            byte[] aesKey = Convert.FromBase64String(ReadEncryptionKey(companyCode));
            byte[] aesIV = Convert.FromBase64String(ReadIVKey(companyCode));

            using var aesManaged = new AesManaged()
            {
                BlockSize = 128,
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                Key = aesKey,
                IV = aesIV
            };
            ICryptoTransform decryptor = aesManaged.CreateDecryptor(aesKey, aesIV);
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(encryptedData));
            using var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return await streamReader.ReadToEndAsync();
        }
    }
}
