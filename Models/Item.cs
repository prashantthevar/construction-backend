using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApiApp.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
