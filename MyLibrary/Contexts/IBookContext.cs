using MongoDB.Driver;
using MyLibrary.Models;

namespace MyLibrary.Contexts
{
    public interface IBookContext
    {
        IMongoCollection<Book> Books { get; }
    }
}
