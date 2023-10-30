using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BSES.DocumentManagementSystem.Data.Models
{
    public class AccessLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string DocumentID { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public int ActionTaken { get; set; }
    }
}
