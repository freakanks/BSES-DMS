using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSES.DocumentManagementSystem.Entities.Contracts
{
    public interface IBaseEntity
    {
        public string CreatedUserID { get; set; }
        public string UpdatedUserID { get; set; }
        public DateTime CreatedDateTime { get; set;}
        public DateTime UpdatedDateTime { get; set;}
        public RecordStatusCode RecordStatusCode { get; set; }
    }
    public enum RecordStatusCode
    {
        Active,
        InActive,
        Deleted
    }
}
