using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Models;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController(VideoGameDbContext context) : ControllerBase
    {
        protected readonly VideoGameDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<Publisher>> GetPublishers()
        {
            var publishers = await _context.Publishers.ToListAsync();

            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisherById(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher is null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> AddPublisher(Publisher newPublisher)
        {
            if(newPublisher is null)
            {
                return BadRequest("Publisher cannot be null.");
            }

            _context.Publishers.Add(newPublisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublisherById), new { id = newPublisher.Id }, newPublisher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Publisher>> UpdatePublisher(int id, Publisher updatedPublisher)
        {
            var existingPublisher = await _context.Publishers.FindAsync(id);
            if (existingPublisher is null)
            {
                return NotFound();
            }

            existingPublisher.Name = updatedPublisher.Name;
            await _context.SaveChangesAsync();

            return Ok(updatedPublisher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Publisher>> DeletePublisher(int id)
        {
            var existingPublisher = await _context.Publishers.FindAsync(id);
            if (existingPublisher is null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(existingPublisher);
            await _context.SaveChangesAsync();

            return Ok(existingPublisher);
        }
    }
}
