using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrontBack.Server.Data;
using FrontBack.Server.Models;

namespace Backend.Controllers
{
    public interface IGenreManager
    {
        Task<List<Genre>> GetGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task<bool> UpdateGenreAsync(int id, Genre Genre);
        Task<Genre> CreateGenreAsync(Genre Genre);
        Task<bool> DeleteGenreAsync(int id);
    }

    public class GenreManager : IGenreManager
    {
        private readonly AppDbContext _context;

        public GenreManager(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            return await _context.Genre.ToListAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _context.Genre.FindAsync(id);
        }

        public async Task<bool> UpdateGenreAsync(int id, Genre Genre)
        {
            if (id != Genre.Id)
            {
                return false;
            }

            _context.Entry(Genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Genre> CreateGenreAsync(Genre Genre)
        {
            _context.Genre.Add(Genre);
            await _context.SaveChangesAsync();
            return Genre;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var Genre = await _context.Genre.FindAsync(id);
            if (Genre == null)
            {
                return false;
            }

            _context.Genre.Remove(Genre);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool GenreExists(int id)
        {
            return _context.Genre.Any(e => e.Id == id);
        }
    }


    [Route("api/[controller]")]
    [ApiController]

    public class GenresController : ControllerBase
    {
        private readonly IGenreManager _GenreManager;

        public GenresController(IGenreManager GenreManager)
        {
            _GenreManager = GenreManager;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var Genres = await _GenreManager.GetGenresAsync();
            return Ok(Genres);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var Genre = await _GenreManager.GetGenreByIdAsync(id);

            if (Genre == null)
            {
                return NotFound();
            }

            return Ok(Genre);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre Genre)
        {
            if (id != Genre.Id)
            {
                return BadRequest();
            }

            var success = await _GenreManager.UpdateGenreAsync(id, Genre);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre Genre)
        {
            var createdGenre = await _GenreManager.CreateGenreAsync(Genre);
            return CreatedAtAction("GetGenre", new { id = createdGenre.Id }, createdGenre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var success = await _GenreManager.DeleteGenreAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
