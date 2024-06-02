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
    public interface IMangaManager
    {
        Task<List<Manga>> GetMangasAsync();
        Task<Manga> GetMangaByIdAsync(int id);
        Task<bool> UpdateMangaAsync(int id, Manga Manga);
        Task<Manga> CreateMangaAsync(Manga Manga);
        Task<bool> DeleteMangaAsync(int id);
    }

    public class MangaManager : IMangaManager
    {
        private readonly AppDbContext _context;

        public MangaManager(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Manga>> GetMangasAsync()
        {
            return await _context.Mangas.Include(m => m.Genre).Include(m => m.Author).ToListAsync();
        }

        public async Task<Manga> GetMangaByIdAsync(int id)
        {
            return await _context.Mangas.FindAsync(id);
        }

        public async Task<bool> UpdateMangaAsync(int id, Manga Manga)
        {
            if (id != Manga.Id)
            {
                return false;
            }

            _context.Entry(Manga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MangaExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Manga> CreateMangaAsync(Manga Manga)
        {
            _context.Mangas.Add(Manga);
            await _context.SaveChangesAsync();
            return Manga;
        }

        public async Task<bool> DeleteMangaAsync(int id)
        {
            var Manga = await _context.Mangas.FindAsync(id);
            if (Manga == null)
            {
                return false;
            }

            _context.Mangas.Remove(Manga);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool MangaExists(int id)
        {
            return _context.Mangas.Any(e => e.Id == id);
        }
    }


    [Route("api/[controller]")]
    [ApiController]

    public class MangasController : ControllerBase
    {
        private readonly IMangaManager _MangaManager;

        public MangasController(IMangaManager MangaManager)
        {
            _MangaManager = MangaManager;
        }

        // GET: api/Mangas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangas()
        {
            var Mangas = await _MangaManager.GetMangasAsync();
            return Ok(Mangas);
        }

        // GET: api/Mangas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manga>> GetManga(int id)
        {
            var Manga = await _MangaManager.GetMangaByIdAsync(id);

            if (Manga == null)
            {
                return NotFound();
            }

            return Ok(Manga);
        }

        // PUT: api/Mangas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManga(int id, Manga Manga)
        {
            if (id != Manga.Id)
            {
                return BadRequest();
            }

            var success = await _MangaManager.UpdateMangaAsync(id, Manga);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Mangas
        [HttpPost]
        public async Task<ActionResult<Manga>> PostManga(Manga Manga)
        {
            var createdManga = await _MangaManager.CreateMangaAsync(Manga);
            return CreatedAtAction("GetManga", new { id = createdManga.Id }, createdManga);
        }

        // DELETE: api/Mangas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManga(int id)
        {
            var success = await _MangaManager.DeleteMangaAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
