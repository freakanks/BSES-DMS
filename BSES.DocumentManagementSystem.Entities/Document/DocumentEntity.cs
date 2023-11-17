using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Entities
{
    ///<inheritdoc/>
    public class DocumentEntity : BaseEntity, IDocumentEntity
    {
        public string DocumentID { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        public DocumentType DocumentType { get; set; } = DocumentType.PDF;
        public int Documentversion { get; set; } = 1;
        public DocumentAccessScope DocumentAccessScope { get; set; } = DocumentAccessScope.Internal;
        public IEnumerable<IDocumentUserEntity> Users { get; set; } = Enumerable.Empty<IDocumentUserEntity>();
        public DocumentCategory Category { get; set; } = DocumentCategory.KYC;
        public long Year { get; set; } = DateTime.Now.Year;
    }
}
