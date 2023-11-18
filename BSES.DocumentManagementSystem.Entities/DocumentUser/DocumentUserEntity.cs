using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Entities
{
    ///<inheritdoc/>
    public class DocumentUserEntity : BaseEntity, IDocumentUserEntity
    {
        public DocumentUserEntity(string userID,
                                  string userName,
                                  string secretKey,
                                  string companyCode,
                                  bool isAuthenticated,
                                  DocumentUserRight userRight,
                                  UserAccessScope userAccessScope)
        {
            UserID = userID;
            UserName = userName;
            SecretKey = secretKey;
            CompanyCode = companyCode;
            IsAuthenticated = isAuthenticated;
            UserRight = userRight;
            UserAccessScope = userAccessScope;
        }

        public string UserID { get; }
        public string UserName { get; }
        public string SecretKey { get; }
        public string CompanyCode { get; }
        public bool IsAuthenticated { get; }
        public DocumentUserRight UserRight { get; set; } = DocumentUserRight.ReadAccess;
        public UserAccessScope UserAccessScope { get; set; } = UserAccessScope.InternalUser;

    }
}
