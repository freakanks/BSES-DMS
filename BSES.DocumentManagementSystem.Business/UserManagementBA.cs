using BSES.DocumentManagementSystem.Business.Contracts;
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
        /// Default Constructor.
        /// </summary>
        /// <param name="logger"></param>
        public UserManagementBA(ILogger<UserManagementBA> logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
        public Task<IDocumentUserEntity> GetDocumentUser(string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public Task<string> SaveDocumentUser(IDocumentUserEntity userEntity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
