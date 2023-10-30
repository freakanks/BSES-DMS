using BSES.DocumentManagementSystem.Business.Contracts;
using Microsoft.Extensions.Logging;

namespace BSES.DocumentManagementSystem.Business
{
    ///<inheritdoc/>
    public class EncryptorDecryptorBA : IEncryptorDecryptorBA
    {
        /// <summary>
        /// Readonly instance for logger.
        /// </summary>
        private readonly ILogger<EncryptorDecryptorBA> _logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        public EncryptorDecryptorBA(ILogger<EncryptorDecryptorBA> logger)
        {
            _logger = logger;
        }


        ///<inheritdoc/>
        public Stream Decrypt(Stream stream)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public Stream Encrypt(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
