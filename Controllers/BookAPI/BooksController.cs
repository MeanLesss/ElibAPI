// Tongsreng TAL, [30 - Nov - 22 9:08 AM]
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELibAPI.Filters;
using ElibAPI.Data;
using ElibAPI.Models;
using ELibAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using ElibAPI.Controllers;

namespace ELibAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ApiBaseController
    {
        private readonly LibDbContext _context;
        private readonly IWebHostEnvironment env;

        public BooksController(LibDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: api/Books
        [HttpGet("get-all-Book")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { status = "FAILED", error = "Invalid token." });
            }

            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("get-book/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return BadRequest(new { error="No item found as you expected!"});
            }

            return book;
        }

        [HttpPost("create-book")]
        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostBook([FromForm] BookDto book, IFormFile? file)
        {
            if (string.IsNullOrEmpty(book.Title) || book.UserId == null || book.group_id == null)
            {
                return BadRequest(new { status = "FAILED", error = "Missing required fields." });
            }

            var newBook = new Book
            {
                Title = book.Title,
                UserId = book.UserId.Value,
                GroupId = book.group_id.Value
            };

            // Handle file upload (if provided)
            if (file != null && file.Length > 0)
            {
                var uploadFolder = Path.Combine(env.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                if (file != null)
                {
                    var realPath = Path.Combine(env.WebRootPath,
                        "Uploads",
                        file.FileName);
                    using var fs = System.IO.File.OpenWrite(realPath);
                    await file.CopyToAsync(fs);
                    newBook.Path = Path.Combine("Uploads",
                        file.FileName);
                } 
                // Set file path in the book object
                newBook.Path = Path.Combine("Uploads", file.FileName);
            }
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
             
            return CreatedAtAction(nameof(PostBook), new { id = newBook.Id }, new { status = "SUCCESS", message = "Book created successfully.", book = newBook });
        }



        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("update-book")]
        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Book>> AddBook([FromForm()] BookDto book, IFormFile file)
        { 
            if (book.Id == null || string.IsNullOrEmpty(book.Title) || book.UserId == null || book.group_id == null)
            {
                return BadRequest(new { status = "FAILED", error = "Missing required fields." });
            }

            var exitingBook = await _context.Books.FirstOrDefaultAsync(x=>x.Id == book.Id);

            exitingBook.Title = book.Title;
            exitingBook.UserId = book.UserId.Value;
            exitingBook.GroupId = book.group_id.Value;

            // Handle file upload (if provided)
            if (file != null && file.Length > 0)
            {
                var uploadFolder = Path.Combine(env.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                if (file != null)
                {
                    var realPath = Path.Combine(env.WebRootPath,
                        "Uploads",
                        file.FileName);
                    using var fs = System.IO.File.OpenWrite(realPath);
                    await file.CopyToAsync(fs);
                    exitingBook.Path = Path.Combine("Uploads",
                        file.FileName);
                }
                // Set file path in the book object
                exitingBook.Path = Path.Combine("Uploads", file.FileName);
            }
            _context.Books.Update(exitingBook);
            await _context.SaveChangesAsync();
  

            return CreatedAtAction(nameof(PostBook), new { id = exitingBook.Id }, new { status = "SUCCESS", message = "Book update successfully.", book = exitingBook });
        }
         
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new
                {
                    message = "Failed to delete!"
                });
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Successfully deleted!"
            });
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}