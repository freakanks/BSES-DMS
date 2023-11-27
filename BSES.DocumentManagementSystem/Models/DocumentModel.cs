using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Entities.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BSES.DocumentManagementSystem
{
    public class DocumentModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Document Name is required.")]
        public string? DocumentName { get; set; }

        [Required(ErrorMessage = "Cannot process blank stream of data.")]
        public IFormFile? FileInfo { get; set; }
        public DocumentAccessScope DocumentAccessScope { get; set; } = DocumentAccessScope.Internal;
        public DocumentCategory DocumentCategory { get; set; } = DocumentCategory.KYC;
    }
}
