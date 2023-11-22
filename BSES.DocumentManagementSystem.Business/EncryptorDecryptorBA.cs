using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.Collections.Concurrent;
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
        private ConcurrentDictionary<string, string> _encryptionKeys = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Local dictionary to keep hold of all the encryption IV for companies.
        /// </summary>
        private ConcurrentDictionary<string, string> _IV = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Readonly instance for logger.
        /// </summary>
        private readonly ILogger<EncryptorDecryptorBA> _logger;

        /// <summary>
        /// Gets or read 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        private string ReadEncryptionKey(string companyCode) =>
                    _encryptionKeys.GetOrAdd(companyCode, (companyCode) =>
                                 Convert.ToString(Registry.LocalMachine.OpenSubKey($"{DMSConstants.HKEY_BSES_RAJDHANI}\\{companyCode}").GetValue(DMSConstants.DMSEncryptionKey))
                    );

        private string ReadIVKey(string companyCode) =>
                    _IV.GetOrAdd(companyCode, (companyCode) => $"{DMSConstants.HKEY_BSES_RAJDHANI}\\{companyCode}\\{DMSConstants.DMSEncryptionKey}");


        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        public EncryptorDecryptorBA(ILogger<EncryptorDecryptorBA> logger)
        {
            _logger = logger;
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
            byte[] aesKey = new byte[16];
            byte[] aesIV = new byte[16];

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
            byte[] aesKey = new byte[16];
            byte[] aesIV = new byte[16];

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
