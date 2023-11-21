using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Data.Contracts;
using BSES.DocumentManagementSystem.Entities.Contracts;
using Microsoft.Extensions.Logging;

namespace BSES.DocumentManagementSystem.Business
{
    ///<inheritdoc/>
    public class UserManagementBA : IUserManagementBA
    {
        /// <summary>
        /// Readonly instaance for logging.
        /// </summary>
        private readonly ILogger<UserManagementBA> _logger;

        /// <summary>
        /// Readonly instance for data access of User Management.
        /// </summary>
        private readonly IDocumentUserEntityDA _userEntityDA;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userEntityDA"></param>
        public UserManagementBA(ILogger<UserManagementBA> logger, IDocumentUserEntityDA userEntityDA)
        {
            _logger = logger;
            _userEntityDA = userEntityDA;
        }

        public async Task<IDocumentUserEntity?> AuthenticateDocumentUserAsync(string userName, string secretKey, CancellationToken cancellationToken)
        {
            try
            {
                return await _userEntityDA.AuthenticateUserAsync(userName, secretKey, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
            return null;
        }

        ///<inheritdoc/>
        public async Task<IDocumentUserEntity?> GetDocumentUserAsync(string userName, CancellationToken cancellationToken)
        {
            try
            {
                return await _userEntityDA.GetAsync(userName, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
            return null;
        }

        ///<inheritdoc/>
        public async Task<IDocumentUserEntity?> SaveDocumentUserAsync(IDocumentUserEntity userEntity, CancellationToken cancellationToken)
        {
            try
            {
               return await _userEntityDA.SaveAsync(userEntity, cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex}");
            }
            return null;
        }
    }
}
