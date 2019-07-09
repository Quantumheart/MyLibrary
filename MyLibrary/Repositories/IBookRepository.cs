using MyLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(string id);
        Task CreateBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(string id);
    }
}
