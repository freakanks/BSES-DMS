using System.ComponentModel.DataAnnotations;

namespace BSES.DocumentManagementSystem.Data
{
    /// <summary>
    /// POCO for document entity.
    /// </summary>
    public class Document : BaseRecord
    {
        /// <summary>
        /// Unique ID for document.
        /// </summary>
        [Key]
        public string DocumentID { get; set; }
        /// <summary>
        /// Name of the document.
        /// </summary>
        [Required]
        public string DocumentName { get; set; }
        /// <summary>
        /// Absolute path for the document.
        /// </summary>
        [Required]
        public string DocumentPath { get; set; }
        /// <summary>
        /// Year on which document is stored.
        /// </summary>
        public long Year { get; set; } = DateTime.Now.Year;
        /// <summary>
        /// Category of the document.
        /// </summary>
        public int? Category { get; set; }
        /// <summary>
        /// Type of the document.
        /// </summary>
        public int? DocumentType { get; set; }
        /// <summary>
        /// Versioning of the document.
        /// </summary>
        public int DocumentVersion { get; set; } = 1;
        /// <summary>
        /// Access scope for the document.
        /// </summary>
        public int DocumentAccessScope { get; set; } = 1;
        /// <summary>
        /// Comma seperated user ids attached.
        /// </summary>
        public string? Users { get; set; }
        /// <summary>
        /// Flag if the document has been archived.
        /// </summary>
        public bool IsArchived {  get; set; }
    }
}
