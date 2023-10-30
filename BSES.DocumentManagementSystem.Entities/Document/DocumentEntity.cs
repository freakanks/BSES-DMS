using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Entities
{
    public class DocumentEntity: BaseEntity, IDocumentEntity
    {
        public string DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }       
        public DocumentType DocumentType { get; set; }
        public int Documentversion { get; set; }
        public DocumentAccessScope DocumentAccessScope { get; set; }
        public IEnumerable<IDocumentUserEntity> Users { get; set; } = Enumerable.Empty<IDocumentUserEntity>();
    }
}
