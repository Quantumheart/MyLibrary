using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyLibrary.Models
{
    // CM 07/23/2019 
    // Book model for the books to be added to the application
    public class Book
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Title")]
        [BsonRequired]
        public string Title { get; set; }
        [BsonElement("Author")]
        [BsonRequired]
        public string Author { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Updated")]
        public string UpdatedAt { get; set; }
        [BsonElement("Read")]
        public string Read { get; set; }
    }
}
