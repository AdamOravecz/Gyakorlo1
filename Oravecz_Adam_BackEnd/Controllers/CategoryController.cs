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
    public class CategoryController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public CategoryController(LibrarydbContext context)
        {
            _context = context;
        }

        // GET api/category
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories
                    .Include(c => c.Books)
                    .ToListAsync();

                var result = categories.Select(c => new
                {
                    c.CategoryId,
                    c.CategoryName,
                    Books = c.Books.Select(b => new
                    {
                        b.BookId,
                        b.Title,
                        b.PublishDate,
                        b.AuthorId,
                        b.CategoryId
                    }).ToList()
                }).ToList();

                return StatusCode(200, new { Message = "Categories retrieved", result });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
    }
}
