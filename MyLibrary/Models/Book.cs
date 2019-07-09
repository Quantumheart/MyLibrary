using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyLibrary.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Author")]
        public string Author { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Updated")]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("Read")]
        public string Read { get; set; }
    }
}
