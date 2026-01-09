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
    public class AuthorController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public AuthorController(LibrarydbContext context)
        {
            _context = context;
        }
        [HttpGet("{authorName}")]
        public async Task<IActionResult> GetAuthorByName(string authorName)
        {
            try
            {
                var searchName = authorName;

                var author = await _context.Authors
                    .Include(a => a.Books)
                    .FirstOrDefaultAsync(a => a.AuthorName != null && a.AuthorName == searchName);

                if (author != null)
                {
                    var result = new
                    {
                        author.AuthorId,
                        author.AuthorName,
                        Books = author.Books.Select(b => new
                        {
                            b.BookId,
                            b.Title,
                            b.PublishDate,
                            b.CategoryId
                        }).ToList()
                    };

                    return StatusCode(200, new { Message = "Author found", result });
                }

                return StatusCode(400, new { Message = $"Author '{authorName}' not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetAuthorCount()
        {
            try
            {
                var authorCount = await _context.Authors.CountAsync();
                return StatusCode(200, new { Message = "Szerzők Száma: ", AuthorCount = authorCount });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = "Nem lehet csatlakozni az adatbázishoz"});
            }
        }
    }
}
