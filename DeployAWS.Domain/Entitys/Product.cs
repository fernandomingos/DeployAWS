using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeployAWS.Domain.Entitys
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public decimal Value { get; set; }
        [BsonElement]
        public bool IsAvaiable { get; set; }
        [BsonElement]
        public int Amount { get; set; }
    }
}
