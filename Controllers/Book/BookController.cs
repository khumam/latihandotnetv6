using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagement.Models;

namespace BookManagement.Controllers.Todo
{
  [Route("api/[controller]")]
  [ApiController]
  public class BookController : ControllerBase
  {
    private readonly BookContext _context;

    public BookController(BookContext context)
    {
      _context = context;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
      if (_context.Books == null)
      {
        return NotFound();
      }
      return await _context.Books.ToListAsync();
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(long id)
    {
      if (_context.Books == null)
      {
        return NotFound();
      }
      var Book = await _context.Books.FindAsync(id);

      if (Book == null)
      {
        return NotFound();
      }

      return Book;
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(long id, Book Book)
    {
      if (id != Book.Id)
      {
        return BadRequest();
      }

      _context.Entry(Book).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
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

      return NoContent();
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book Book)
    {
      if (_context.Books == null)
      {
        return Problem("Entity set 'TodoContext.Books'  is null.");
      }
      _context.Books.Add(Book);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetBook), new { id = Book.Id }, Book);
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(long id)
    {
      if (_context.Books == null)
      {
        return NotFound();
      }
      var Book = await _context.Books.FindAsync(id);
      if (Book == null)
      {
        return NotFound();
      }

      _context.Books.Remove(Book);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool BookExists(long id)
    {
      return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
