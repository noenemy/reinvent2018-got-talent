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
    public class GameResultsController : ControllerBase
    {
        private DataContext _context;

        public GameResultsController(DataContext context)
        {
            _context = context;
        }

        // GET api/gameresults
        [HttpGet]
        public async Task<IActionResult> GetGameResults()
        {
            var values = await _context.GameResult.ToListAsync();
            return Ok(values);
        }

        // GET api/gameresults/5
        [HttpGet("{game_id}")]
        public async Task<IActionResult> GetGameResult(int game_id)
        {
            var value = await _context.GameResult.FirstOrDefaultAsync(x => x.game_id == game_id);
            return Ok(value);
        }

        // POST api/gameresults
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/gameresults/5
        [HttpPut("{game_id}")]
        public void Put(int game_id, [FromBody] string value)
        {
        }

        // DELETE api/gameresults/5
        [HttpDelete("{game_id}")]
        public void Delete(int game_id)
        {
        }
    }            
}