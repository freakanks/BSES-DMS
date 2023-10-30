using BSES.DocumentManagementSystem.Entities.Contracts;
using System.Diagnostics.SymbolStore;

namespace BSES.DocumentManagementSystem.Entities
{
    public class DocumentUserEntity : BaseEntity, IDocumentUserEntity
    {
        public DocumentUserEntity(string userID,
                                  string userName,
                                  string secretKey,
                                  bool isAuthenticated)
        {
            UserID = userID;
            UserName = userName;
            SecretKey = secretKey;
            IsAuthenticated = isAuthenticated;
        }

        public string UserID { get; }
        public string UserName { get; }
        public string SecretKey { get; }
        public bool IsAuthenticated { get; }
        public DocumentUserRight UserRight { get; set; }
    }
}
