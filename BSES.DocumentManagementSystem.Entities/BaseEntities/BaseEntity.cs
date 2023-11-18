using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public string CreatedUserID { get; set; } = "System";
        public string UpdatedUserID { get; set; } = "Sytem";
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
        public RecordStatusCode RecordStatusCode { get; set; } = RecordStatusCode.Active;
    }
}
