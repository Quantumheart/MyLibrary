using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MongoDB.Driver;
using MyLibrary.Contexts;
using MyLibrary.Models;

namespace MyLibrary.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IBookContext _context;

        public BookRepository(IBookContext context)
        {
            _context = context;
        }

        public async Task<Book> GetBook(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(m => m.Id, id);


            return await _context.Books.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.Find(_ => true).ToListAsync();
        }

        public async Task CreateBook(Book book)
        {
            await _context.Books.InsertOneAsync(book);
        }

        public async Task<bool> DeleteBook(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Books
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public Task<bool> UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }


    }
}
