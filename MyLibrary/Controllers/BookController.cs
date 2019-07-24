using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Models;
using MyLibrary.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyLibrary.Controllers
{

    // CM 07/22/2019
    // This controller can only be accessed through a JWTToken Bearer
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // CM 07/23/2019 
        // Returns a list of books to the application user
        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetRecords()
        {
            return new ObjectResult(await _bookRepository.GetBooks());
        }

        // CM 07/23/2019 
        // Returns a book to the application user
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

        // CM 07/23/2019 
        // Creates a book in the database
        // POST: api/book
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody]Book book)
        {
            book.UpdatedAt = DateTime.Now.ToString(format: "yyyy-MM-dd");
            //                      true            false
            // is this condition ? ref consequent : ref alternative
            book.Read = book.Read == null ? "Not read" : "Read";
            await _bookRepository.CreateBook(book);
            return new OkObjectResult(book);
        }

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // CM 07/23/2019 
        // Deletes a book in the database
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
