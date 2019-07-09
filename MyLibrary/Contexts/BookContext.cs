using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyLibrary.Models;

namespace MyLibrary.Contexts
{
    public class BookContext : IBookContext
    {
        private readonly IMongoDatabase _db;

        public BookContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Book> Books => _db.GetCollection<Book>("Books");
    }
}
