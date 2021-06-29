using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoresWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using BookStoresWebAPI.Helpers;

namespace BookStoresWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoresDBContext _context;

        public AuthorsController(BookStoresDBContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet("GetAuthors")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            //await Task.Delay(3000);
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("GetAuthorsCount")]
        public async Task<ActionResult<ItemCount>> GetAuthorsCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.Authors.Count();
            return await Task.FromResult(itemCount);
        }

        // GET: api/Authors
        [HttpGet("GetAuthorsByPage")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsByPage([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Authors.AsQueryable();

            if (pagination.AuthorId != 0)
                queryable = queryable.Where(a => a.AuthorId == pagination.AuthorId);
            if (!string.IsNullOrEmpty(pagination.FirstName))
                queryable = queryable.Where(a => a.FirstName.ToUpper().Contains(pagination.FirstName.ToUpper()));
            if (!string.IsNullOrEmpty(pagination.LastName))
                queryable = queryable.Where(a => a.LastName.ToUpper().Contains(pagination.LastName.ToUpper()));
            if (!string.IsNullOrEmpty(pagination.City))
                queryable = queryable.Where(a => a.City.ToUpper().Contains(pagination.City.ToUpper()));
            if (!string.IsNullOrEmpty(pagination.EmailAddress))
                queryable = queryable.Where(a => a.EmailAddress.ToUpper().Contains(pagination.EmailAddress.ToUpper()));
            await HttpContext.InsertPaginationParamInResponse(queryable, pagination.QuantityPerPage);

            var result = queryable.Paginate(pagination);
            return result.ToList();
        }

        // GET: api/Authors/5
        [HttpGet("GetAuthor/{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateAuthor/{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return author;
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
