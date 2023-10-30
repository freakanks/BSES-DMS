using BSES.DocumentManagementSystem.Common;

namespace BSES.DocumentManagementSystem.Entities.Contracts
{
    public interface IDocumentEntity: IBaseEntity
    {
        public string DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }       
        public DocumentType DocumentType { get; set; }
        public int Documentversion { get; set; }
        public DocumentAccessScope DocumentAccessScope { get; set; }
        public IEnumerable<IDocumentUserEntity> Users { get; set; }
    }
    public enum DocumentType
    {
        PDF,
        PNG,
        JPG,
        JPEG,
        Others
    }   
}
