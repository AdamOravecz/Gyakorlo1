using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oravecz_Adam_BackEnd.Models;

namespace Oravecz_Adam_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public BookController(LibrarydbContext context)
        {
            _context = context;
        }

        // GET api/book
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .ToListAsync();

                var result = books.Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.PublishDate,
                    b.AuthorId,
                    AuthorName = b.Author?.AuthorName,
                    b.CategoryId,
                    CategoryName = b.Category?.CategoryName
                }).ToList();

                return StatusCode(200, new { Message = "Books retrieved", result });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = "Unable to connect to any of the specified MySQL hosts" });
            }
        }
    }
}
