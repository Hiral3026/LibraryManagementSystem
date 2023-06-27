using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LibraryManagementProject.Models;
using System.Diagnostics;


namespace LibraryManagementProject.Controllers
{
    public class BookDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BookData/ListBooks
        [HttpGet]
        [Route("api/BookData/ListBooks")]
        public IHttpActionResult GetBooks()
        {
            try
            {
                List<Book> books = db.Books.OrderBy(b => b.BookId).ToList();
                List<BookDto> bookDtos = new List<BookDto>();

                foreach (Book book in books)
                {
                    bookDtos.Add(new BookDto()
                    {
                        BookId = book.BookId,
                        Title = book.Title,
                        Author = book.Author,
                        Category = book.Category,
                        AvailableCopy = book.AvailableCopy
                    });
                }

                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return InternalServerError(ex);
            }
        }


        // GET: api/BookData/FindBook/5
        [ResponseType(typeof(Book))]
        [HttpGet]
        [Route("api/BookData/FindBook/{id}")]
        public IHttpActionResult FindBook(int id)
        {
            Book Book = db.Books.Find(id);

            if (Book == null)
            {
                return NotFound();
            }

            BookDto BookDto = new BookDto()
            {
                BookId = Book.BookId,
                Title = Book.Title,
                Author = Book.Author,
                Category = Book.Category,
                AvailableCopy = Book.AvailableCopy
            };

            return Ok(BookDto);
        }


        // POST: api/BookData/UpdateBook/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBook(int id, Book book)
        {
            Debug.WriteLine("I have reached the update book method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != book.BookId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + book.BookId);
                Debug.WriteLine("POST parameter" + book.Title);
                Debug.WriteLine("POST parameter " + book.Author);
                Debug.WriteLine("POST parameter " + book.Category);
                Debug.WriteLine("POST parameter " + book.AvailableCopy);
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BookData/AddBook
        [ResponseType(typeof(Book))]
        [HttpPost]
        [Route("api/BookData/AddBook")]
        public IHttpActionResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("GetBook", new { id = book.BookId }, book);
        }



        // DELETE: api/BookData/DeleteBook/5
        [ResponseType(typeof(Book))]
        [HttpPost]
        [Route("api/BookData/DeleteBook/{id}")]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}