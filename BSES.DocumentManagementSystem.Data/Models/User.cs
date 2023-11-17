using System.ComponentModel.DataAnnotations;

namespace BSES.DocumentManagementSystem.Data
{
    /// <summary>
    /// POCO entity for the user.
    /// </summary>
    public class User : BaseRecord
    {
        /// <summary>
        /// Unique UserID.
        /// </summary>
        [Key]
        public string UserID { get; set; }
        /// <summary>
        /// Name of the user.
        /// </summary>                
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Secret Key for authentication of the user.
        /// </summary>
        [Required]
        public string SecretKey { get; set; }
        /// <summary>
        /// Access rights of the user.
        /// </summary>
        public int UserRight { get; set; } = 0;
        /// <summary>
        /// Access Scope for the User.
        /// </summary>
        public int UserAccessScope { get; set; } = 1;

    }
}
