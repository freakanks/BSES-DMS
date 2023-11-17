namespace BSES.DocumentManagementSystem.Entities.Contracts.Document
{
    public interface IDocumentLogEntity: IBaseEntity
    {
        public long LogId { get; set; }
        public string DocumentID { get; set; }
        public string UserId { get; set; }
        public DocumentAction ActionTaken { get; set; }
    }
    public enum DocumentAction
    {
        Read,
        Write,
        Update,
        Delete
    }
}
