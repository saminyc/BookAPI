using BookApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        static private List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell", YearPublished = 1949 },
            new Book { Id = 2, Title = "To Kill a Mockingbird"},
            new Book { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", YearPublished = 1925 },
            new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", YearPublished = 1813 },
            new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", YearPublished = 1951 },
            new Book { Id = 6, Title = "Brave New World", Author = "Aldous Huxley", YearPublished = 1932 }
        };
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(books);
        }

        // GET api/books/id

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book==null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books/id
        [HttpPost]

        public ActionResult<Book> AddBook(Book newBook)
        {
            // Handle null book
            if (newBook == null)
            {
                return BadRequest("Book cannot be null");
            }
            else
            {
                books.Add(newBook);// Add a new book to the list
                return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
            }
        }

        // PUT api/books/id
        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var findBook = books.FirstOrDefault(x => x.Id == id);// Find the book by id
            if (findBook == null)
            {
                return NotFound();
            }
            // Update the book details
            findBook.Id=updatedBook.Id;
            findBook.Title = updatedBook.Title;
            findBook.Author = updatedBook.Author;   
            findBook.YearPublished = updatedBook.YearPublished;

            return NoContent(); // Returns no content status code

        }

        // DELETE api/books/id
        public IActionResult DeleteBook(int id)
        {
            var chosenBook = books.FirstOrDefault(x=>x.Id==id);
            if (chosenBook == null)
            {
                return NotFound(); // If the book is not found, return 404
            }
            books.Remove(chosenBook); // Remove the book from the list
            return NoContent(); // Returns no content status code   

        }
    }
}
