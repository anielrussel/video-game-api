using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Models;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameDetailsController(VideoGameDbContext context) : ControllerBase
    {
        protected readonly VideoGameDbContext _context = context;


        [HttpGet]
        public async Task<ActionResult<VideoGameDetails>> GetVideoGameDetails()
        {
            var details = await _context.VideoGameDetails.ToListAsync();

            return Ok(details);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGameDetails>> GetVideoGameDetailsById(int id)
        {
            var details = await _context.VideoGameDetails.FindAsync(id);
            if (details is null)
            {
                return NotFound();
            }
            return Ok(details);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVideoGameDetails(VideoGameDetails newVideoGameDetails)
        {
            if (newVideoGameDetails is null)
            {
                return BadRequest("Video game details cannot be null.");
            }
            
            _context.VideoGameDetails.Add(newVideoGameDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVideoGameDetailsById), new { id = newVideoGameDetails.Id }, newVideoGameDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGameDetails(int id, VideoGameDetails updatedVideoGameDetails)
        {
          var existingVideoGameDetails = await _context.VideoGameDetails.FindAsync(id);
            if (existingVideoGameDetails is null)
            {
                return NotFound();
            }
            existingVideoGameDetails.Description = updatedVideoGameDetails.Description;
            existingVideoGameDetails.ReleasedDate = updatedVideoGameDetails.ReleasedDate;
            existingVideoGameDetails.VideoGameId = updatedVideoGameDetails.VideoGameId;
            
            _context.VideoGameDetails.Update(existingVideoGameDetails);
            await _context.SaveChangesAsync();

            return Ok(updatedVideoGameDetails);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGameDetails(int id)
        {
            var existingVideoGameDetails = await _context.VideoGameDetails.FindAsync(id);
            if (existingVideoGameDetails is null)
            {
                return NotFound();
            }

            _context.VideoGameDetails.Remove(existingVideoGameDetails);
            await _context.SaveChangesAsync();

            return Ok(existingVideoGameDetails);
        }
    }
}
