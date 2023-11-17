namespace BSES.DocumentManagementSystem.Data
{
    /// <summary>
    /// POCO entity for the base of every entity.
    /// </summary>
    public class BaseRecord
    {
        /// <summary>
        /// Record Create UserID.
        /// </summary>
        public string CreatedUserID { get; set; } = "DMS Application";
        /// <summary>
        /// Update UserID.
        /// </summary>
        public string UpdatedUserID { get; set; } = "DMS Application";
        /// <summary>
        /// Datetime record created.
        /// </summary>
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Datetime record updated.
        /// </summary>
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Record Status code.
        /// </summary>
        public int RecordStatusCode { get; set; } = 0;
    }
}
