using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Models;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDbContext context) : ControllerBase
    {

        protected readonly VideoGameDbContext _context = context;
   

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            var games = await _context.VideoGames
                .Include(g => g.VideoGameDetails)
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Include(g => g.Genres)
                .ToListAsync();
            
            return Ok(games);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGameById(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);

            if (game is null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newGame)
        {
            if (newGame == null)
            {
                return BadRequest();
            }

            
            _context.VideoGames.Add(newGame);

            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public async  Task<IActionResult> UpdateVideoGame(int id, VideoGame updateGame)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
            {
                return NotFound();
            }

            game.Title = updateGame.Title;
            game.Publisher = updateGame.Publisher;
            game.Developer = updateGame.Developer;
            game.Platform = updateGame.Platform;

            await _context.SaveChangesAsync();

            return Ok(updateGame);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
            {
                return NotFound();
            }

            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();

            return Ok(game);
        }
    }
}
