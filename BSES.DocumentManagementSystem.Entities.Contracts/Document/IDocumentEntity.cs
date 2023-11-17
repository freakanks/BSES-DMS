using BSES.DocumentManagementSystem.Common;

namespace BSES.DocumentManagementSystem.Entities.Contracts
{
    /// <summary>
    /// Information for each document to be saved by the application.
    /// </summary>
    public interface IDocumentEntity : IBaseEntity
    {
        /// <summary>
        /// Unique Identifier of the document.
        /// </summary>
        public string DocumentID { get; set; }
        /// <summary>
        /// Name of the document.
        /// </summary>
        public string DocumentName { get; set; }
        /// <summary>
        /// Absolute path for the document.
        /// </summary>
        public string DocumentPath { get; set; }
        /// <summary>
        /// Year in which the document is saved.
        /// </summary>
        public long Year { get; set; }
        /// <summary>
        /// Category for the document.
        /// </summary>
        public DocumentCategory Category { get; set; }
        /// <summary>
        /// Type of the document.
        /// </summary>
        public DocumentType DocumentType { get; set; }
        /// <summary>
        /// Versioning of the document. Highest is the latest and in decrementing order.
        /// </summary>
        public int Documentversion { get; set; }
        /// <summary>
        /// Access scope for the document, which determines how the document is shared across.
        /// </summary>
        public DocumentAccessScope DocumentAccessScope { get; set; }
        /// <summary>
        /// Explicit users which are external to the system and can access the document.
        /// </summary>
        public IEnumerable<IDocumentUserEntity> Users { get; set; }
    }
    /// <summary>
    /// Enum for the type of documents supported.
    /// </summary>
    public enum DocumentType
    {
        PDF,
        PNG,
        JPG,
        JPEG,
        Others
    }
    /// <summary>
    /// Category of the documents relative to the BSES.
    /// </summary>
    public enum DocumentCategory
    {
        KYC,
        OTH
    }
}
