using BSES.DocumentManagementSystem.Entities.Contracts;

namespace BSES.DocumentManagementSystem.Entities
{
    public class BaseEntity: IBaseEntity
    {
        public string CreatedUserID { get; set; } = string.Empty;
        public string UpdatedUserID { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set;}
        public DateTime UpdatedDateTime { get; set;}
        public RecordStatusCode RecordStatusCode { get; set; }
    }
}
