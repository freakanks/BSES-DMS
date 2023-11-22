using System.ComponentModel.DataAnnotations;

namespace BSES.DocumentManagementSystem.Encryption.Data
{
    /// <summary>
    /// POCO entity for DMS applications Encryption data.
    /// </summary>
    public class DMSEncryptionKey
    {
        /// <summary>
        /// Unique Company Codes.
        /// </summary>
        [Key]
        public string CompanyCode { get; set; }

        /// <summary>
        /// AES encryption key.
        /// </summary>
        [Required]
        public string EncryptionKey { get; set; }

        /// <summary>
        /// AES encryption IV.
        /// </summary>
        [Required]
        public string EncryptionIV { get; set; }
    }
}
