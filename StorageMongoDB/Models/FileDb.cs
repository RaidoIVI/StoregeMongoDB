using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StorageMongoDB.Models
{
    public class FileDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public BsonDocument Description { get; set; }
    }
}
