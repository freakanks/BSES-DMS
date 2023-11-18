
using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Entities
{
    ///<inheritdoc/>
    public class DocumentLogEntity : BaseEntity, IDocumentLogEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DocumentLogEntity(string documentId, string userID, DocumentAction documentAction)
        {
            DocumentID = documentId;
            UserId = userID;
            ActionTaken = documentAction;
        }

        /// <summary>
        /// Paremeterized Constructor.
        /// </summary>
        /// <param name="logID"></param>
        public DocumentLogEntity(long logID, string documentId, string userID, DocumentAction documentAction)
        {
            LogId = logID;
            DocumentID = documentId;
            UserId = userID;
            ActionTaken = documentAction;
        }

        public long LogId { get; }
        public string DocumentID { get; set; }
        public string UserId { get; set; }
        public DocumentAction ActionTaken { get; set; }
    }
}
