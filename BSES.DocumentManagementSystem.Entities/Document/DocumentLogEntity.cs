using BSES.DocumentManagementSystem.Entities.Contracts.Document;

namespace BSES.DocumentManagementSystem.Entities.Document
{
    public class DocumentLogEntity: BaseEntity, IDocumentLogEntity
    {
        public long LogId { get; set; }
        public string DocumentID { get; set; }
        public string UserId { get; set; }
        public string Action {  get; set; }
        public DocumentAction ActionTaken { get; set; }
    }
}
