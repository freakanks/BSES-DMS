using BSES.DocumentManagementSystem.Common;

namespace BSES.DocumentManagementSystem
{
    public class DocumentModel
    {
        public string DocumentName { get; set; }
        public Stream DocumentStream { get; set; }
        public DocumentAccessScope DocumentAccessScope { get; set; } = DocumentAccessScope.Internal;
    }
}
