using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Models;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController(VideoGameDbContext context) : ControllerBase
    {
        protected readonly VideoGameDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<Developer>> GetDevelopers()
        {
            var developers = await _context.Developers.ToListAsync();

            return Ok(developers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloperById(int id)
        {
            var developer = await _context.Developers.FindAsync(id);
            if (developer is null)
            {
                return NotFound();
            }

            return Ok(developer);
        }

        [HttpPost]
        public async Task<ActionResult<Developer>> AddDeveloper(Developer newDeveloper)
        {
            if (newDeveloper is null)
            {
                return BadRequest("Developer cannot be null.");
            }

            _context.Developers.Add(newDeveloper);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeveloperById), new { id = newDeveloper.Id }, newDeveloper);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Developer>> UpdateDeveloper(int id, Developer updatedDeveloper)
        {
            var existingDeveloper = await _context.Developers.FindAsync(id);
            if (existingDeveloper is null)
            {
                return NotFound();
            }

            existingDeveloper.Name = updatedDeveloper.Name;

            await _context.SaveChangesAsync();

            return Ok(updatedDeveloper);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Developer>> DeleteDeveloper(int id)
        {
            var existingDeveloper = await _context.Developers.FindAsync(id);
            if (existingDeveloper is null)
            {
                return NotFound();
            }

            _context.Developers.Remove(existingDeveloper);
            await _context.SaveChangesAsync();

            return Ok(existingDeveloper);
        }
    }
}
