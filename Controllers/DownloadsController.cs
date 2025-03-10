using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElibAPI.Data;
using ElibAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ElibAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadsController : ControllerBase
    {
        private readonly LibDbContext _context;

        public DownloadsController(LibDbContext context)
        {
            _context = context;
        }

        // GET: api/Downloads?book_id=5
        [HttpGet("book/{book_id}/{user_id}")]
        [Authorize]
        public async Task<IActionResult> DownloadFile(int book_id,int user_id)
        { 
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == book_id && x.UserId == user_id);
             
            if (book == null || string.IsNullOrEmpty(book.Path))
            {
                return NotFound(new { status = "FAILED", message = "Book not found." });
            } 

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.Path);  // Adjust this path as needed
             
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { status = "FAILED", message = "File not found." });
            } 
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            await _context.Downloads.AddAsync(new Download { BookId = book_id, UserId = user_id });
            _context.SaveChanges();

            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }


        // GET: api/Downloads/5
        [HttpGet("get-all-download-log")]
        [Authorize]
        public async Task<ActionResult<Download>> GetAllDownloadLog()
        {
            var download = await _context.Downloads.ToListAsync();

            if (download == null)
            {
                return NotFound(new { status = "FAILED", message = $"Download Log not found." });
            }

            return Ok(new { downloadLog = download });
        }
         
    }
}
