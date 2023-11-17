using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Data.Contracts;
using BSES.DocumentManagementSystem.Entities;
using BSES.DocumentManagementSystem.Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BSES.DocumentManagementSystem.Data
{
    ///<inheritdoc/>
    public class DocumentUserEntityDA : IDocumentUserEntityDA
    {
        /// <summary>
        /// Readonly instance for db context.
        /// </summary>
        private readonly DMSDBContext _context;

        /// <summary>
        /// Converts the User data model to entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IDocumentUserEntity? ModelToUserEntity(User? user) => user == null ? null :
            new DocumentUserEntity(user.UserID, user.UserName, user.SecretKey, false,
                Enum.TryParse($"{user.UserRight}", out DocumentUserRight userRight) ? userRight : DocumentUserRight.ReadAccess,
                Enum.TryParse($"{user.UserAccessScope}", out UserAccessScope accessScope) ? accessScope : UserAccessScope.InternalUser)
            {
                CreatedDateTime = user.CreatedDateTime,
                UpdatedDateTime = user.UpdatedDateTime,
                CreatedUserID = user.CreatedUserID,
                UpdatedUserID = user.UpdatedUserID,
                RecordStatusCode = Enum.TryParse($"{user.RecordStatusCode}", out RecordStatusCode statusCode) ? statusCode : 0
            };

        /// <summary>
        /// Converts the entity to User Model.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private User? EntityToUserModel(IDocumentUserEntity? user) => user == null ? null :
            new User()
            {
                UserID = user.UserID,
                SecretKey = user.SecretKey,
                UserName = user.UserName,
                UserAccessScope = (int)user.UserAccessScope,
                UserRight = (int)user.UserRight,

                CreatedDateTime = user.CreatedDateTime,
                UpdatedDateTime = user.UpdatedDateTime,
                CreatedUserID = user.CreatedUserID,
                UpdatedUserID = user.UpdatedUserID,
                RecordStatusCode = (int)user.RecordStatusCode
            };

        public DocumentUserEntityDA(DMSDBContext context)
        {
            _context = context;
        }

        public async Task<bool> RemoveAsync(string userID, CancellationToken cancellationToken) =>
           (await _context.Users.Where(x => x.UserID == userID).ExecuteDeleteAsync(cancellationToken)) > 0;


        public async Task<IDocumentUserEntity> SaveAsync(IDocumentUserEntity user, CancellationToken cancellationToken)
        {
            var model = EntityToUserModel(user);
            if (model == null) return user;

            await _context.Users.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<IDocumentUserEntity> UpdateAsync(string userID, string secretKey, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleAsync(x => x.UserID == userID, cancellationToken);
            user.SecretKey = secretKey;
            await _context.SaveChangesAsync(cancellationToken);
            return ModelToUserEntity(user)!;
        }

        public Task<IDocumentUserEntity> UpdatesAsync(string userID, DocumentUserRight userRights, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IDocumentUserEntity?> GetAsync(string userID, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.UserID == userID).FirstOrDefaultAsync(cancellationToken);
            return ModelToUserEntity(user);
        }
    }
}
