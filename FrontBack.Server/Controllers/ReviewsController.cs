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
    public interface IReviewManager
    {
        Task<List<Review>> GetReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<bool> UpdateReviewAsync(int id, Review Review);
        Task<Review> CreateReviewAsync(Review Review);
        Task<bool> DeleteReviewAsync(int id);
    }

    public class ReviewManager : IReviewManager
    {
        private readonly AppDbContext _context;

        public ReviewManager(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<bool> UpdateReviewAsync(int id, Review Review)
        {
            if (id != Review.Id)
            {
                return false;
            }

            _context.Entry(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Review> CreateReviewAsync(Review Review)
        {
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();
            return Review;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var Review = await _context.Reviews.FindAsync(id);
            if (Review == null)
            {
                return false;
            }

            _context.Reviews.Remove(Review);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }


    [Route("api/[controller]")]
    [ApiController]

    public class ReviewsController : ControllerBase
    {
        private readonly IReviewManager _ReviewManager;

        public ReviewsController(IReviewManager ReviewManager)
        {
            _ReviewManager = ReviewManager;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var Reviews = await _ReviewManager.GetReviewsAsync();
            return Ok(Reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var Review = await _ReviewManager.GetReviewByIdAsync(id);

            if (Review == null)
            {
                return NotFound();
            }

            return Ok(Review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review Review)
        {
            if (id != Review.Id)
            {
                return BadRequest();
            }

            var success = await _ReviewManager.UpdateReviewAsync(id, Review);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review Review)
        {
            var createdReview = await _ReviewManager.CreateReviewAsync(Review);
            return CreatedAtAction("GetReview", new { id = createdReview.Id }, createdReview);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var success = await _ReviewManager.DeleteReviewAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
