using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Models;
using MyLibrary.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/book
        // returns the list of records stored in memory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetRecords()
        {
            return new ObjectResult(await _bookRepository.GetBooks());
        }

        // GET: api/book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            var book = await _bookRepository.GetBook(id);
           
            if (book == null)
            {
                return NotFound();
            }
            return new ObjectResult(book); 
        }

        // POST: api/book
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody]Book book)
        {
            //book.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd");
            await _bookRepository.CreateBook(book);
            return new OkObjectResult(book);
        }

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/book/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var book = await _bookRepository.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }
            await _bookRepository.DeleteBook(book.Id);
            return new OkResult();
        }
    }
}
