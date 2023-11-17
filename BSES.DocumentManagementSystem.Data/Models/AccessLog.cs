using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSES.DocumentManagementSystem.Data
{
    /// <summary>
    /// POCO for the Logs Entity.
    /// </summary>
    public class AccessLog: BaseRecord
    {
        /// <summary>
        /// Increment ID for each log.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        /// <summary>
        /// ID of the document for which the log got created.
        /// </summary>
        [Required]
        public string DocumentID { get; set; }
        /// <summary>
        /// ID of the User for access log.
        /// </summary>
        [Required]
        public string UserId { get; set; }
        /// <summary>
        /// Action ID of action performed.
        /// </summary>
        public int ActionTaken { get; set; } = 0;
    }
}
