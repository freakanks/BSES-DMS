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
                    _encryptionKeys.TryGetValue(companyCode.ToUpper(), out string? value) && !string.IsNullOrEmpty(value) ? value :
                    throw new KeyNotFoundException($"Encryption Key not found for the Company Code {companyCode}");

        /// <summary>
        /// Reads the encryption key IV for a company code.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private string ReadIVKey(string companyCode) =>
                    _IV.TryGetValue(companyCode.ToUpper(), out string? value) && !string.IsNullOrEmpty(value) ? value :
                    throw new KeyNotFoundException($"Encryption Key IV not found for the Company Code {companyCode}");
        
        /// <summary>
        /// Returns a new instance of AESManaged.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        private AesManaged GetAesManaged(string companyCode)
        {
            byte[] aesKey = Convert.FromBase64String(ReadEncryptionKey(companyCode));
            byte[] aesIV = Convert.FromBase64String(ReadIVKey(companyCode));

            return new AesManaged()
            {
                BlockSize = 128,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                KeySize = 256,
                Key = aesKey,
                IV = aesIV
            };
        }

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
            byte[] dataBytes = Encoding.UTF8.GetBytes(inputData);

            using var aesManaged = GetAesManaged(companyCode);
            ICryptoTransform encryptor = aesManaged.CreateEncryptor();

            int blockSize = aesManaged.BlockSize / 8;
            int padding = blockSize - (dataBytes.Length % blockSize);
            byte[] paddedData = new byte[dataBytes.Length + padding];
            Buffer.BlockCopy(dataBytes, 0, paddedData, 0, dataBytes.Length);
            for (int i = dataBytes.Length; i < paddedData.Length; i++)
            {
                paddedData[i] = (byte)padding;
            }

            using var ms = new MemoryStream();
            using var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            await cryptoStream.WriteAsync(paddedData, cancellationToken);
            return Convert.ToBase64String(ms.ToArray());
        }

        public async Task<string> DecryptAsync(string encryptedData, string companyCode, CancellationToken cancellationToken)
        {
            using var aesManaged = GetAesManaged(companyCode);
            ICryptoTransform decryptor = aesManaged.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(encryptedData));
            using var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return await streamReader.ReadToEndAsync();
        }
    }
}
