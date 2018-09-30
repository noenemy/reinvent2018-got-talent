using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GotTalent_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GotTalent_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private DataContext _context;

        public GamesController(DataContext context)
        {
            _context = context;
        }

        // GET api/games
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var values = await _context.Game.ToListAsync();
            return Ok(values);
        }

        // GET api/games/5
        [HttpGet("{game_id}")]
        public async Task<IActionResult> GetUserScore(int game_id)
        {
            var value = await _context.Game.FirstOrDefaultAsync(x => x.game_id == game_id);
            return Ok(value);
        }

        // POST api/games
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/games/5
        [HttpPut("{game_id}")]
        public void Put(int game_id, [FromBody] string value)
        {
        }

        // DELETE api/games/5
        [HttpDelete("{game_id}")]
        public void Delete(int game_id)
        {
        }
    }            
}