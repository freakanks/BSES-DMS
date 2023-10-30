using BSES.DocumentManagementSystem.Common;

namespace BSES.DocumentManagementSystem.Entities.Contracts
{
    /// <summary>
    /// Entity model for user of document management system.
    /// </summary>
    public interface IDocumentUserEntity: IBaseEntity
    {
        /// <summary>
        /// Unique UserID for the user.
        /// </summary>
        public string UserID { get; }
        
        /// <summary>
        /// Username chosen by the user.
        /// </summary>
        public string UserName { get; }
      
        /// <summary>
        /// Secret Hash Key for the user to use for everytime authentication.
        /// </summary>
        public string SecretKey { get; }

        /// <summary>
        /// Flag denoting if the user is autheniticated.
        /// </summary>
        public bool IsAuthenticated { get; }

        /// <summary>
        /// Access rights for the user in DMS.
        /// </summary>
        public DocumentUserRight UserRight { get; set; }

        /// <summary>
        /// Access Scope setting if user is internal team or external.
        /// </summary>
        public UserAccessScope UserAccessScope { get; set; }
    }

    /// <summary>
    /// Enum for the users document rights.
    /// </summary>
    public enum DocumentUserRight
    {
        ReadAccess,
        WriteAccess,
        RemoveAccess,
        UpdateAccess
    }
}
