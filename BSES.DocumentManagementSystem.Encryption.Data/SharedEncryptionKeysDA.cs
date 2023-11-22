using BSES.DocumentManagementSystem.Encryption.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BSES.DocumentManagementSystem.Encryption.Data
{
    ///<inheritdoc/>
    public class SharedEncryptionKeysDA : ISharedEncryptionKeysDA
    {
        /// <summary>
        /// Local Instance for db context of DMS encryption data.
        /// </summary>
        private readonly DMSEncryptionDBContext _context;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="context"></param>
        public SharedEncryptionKeysDA(DMSEncryptionDBContext context)
        {
            _context = context;
        }

        public async Task<IDictionary<string, string>> GetAllEncryptionIVKeysAsync(CancellationToken cancellationToken) =>
                        await _context.DMSEncryptionKeys.ToDictionaryAsync(x => x.CompanyCode, x => x.EncryptionIV, cancellationToken);

        public async Task<IDictionary<string, string>> GetAllEncryptionKeysAsync(CancellationToken cancellationToken) =>
                        await _context.DMSEncryptionKeys.ToDictionaryAsync(x => x.CompanyCode, x => x.EncryptionKey, cancellationToken);

        public Task<string> GetEncryptionIVKeysAsync(string companyCode, CancellationToken cancellationToken) =>
                        _context.DMSEncryptionKeys.Where(x => x.CompanyCode == companyCode).Select(x => x.EncryptionIV).SingleAsync(cancellationToken);

        public Task<string> GetEncryptionKeysAsync(string companyCode, CancellationToken cancellationToken) =>
                        _context.DMSEncryptionKeys.Where(x => x.CompanyCode == companyCode).Select(x => x.EncryptionKey).SingleAsync(cancellationToken);
    }
}
