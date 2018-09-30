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
    public class RankingsController : ControllerBase
    {
        private DataContext _context;

        public RankingsController(DataContext context)
        {
            _context = context;
        }

        // GET api/rankings
        [HttpGet]
        public async Task<IActionResult> GetRankings()
        {
            var values = await _context.GameResult.ToListAsync();
            return Ok(values);
        }

        // GET api/rankings/happy
        [HttpGet("{action_type}")]
        public async Task<IActionResult> GetRankingsByType(string action_type)
        {
            var value = await _context.RankingByType.Select(x => x.action_type == action_type).ToListAsync();
            return Ok(value);
        }

        // POST api/rankings
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/rankings/5
        [HttpPut("{seqNum}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/rankings/5
        [HttpDelete("{seqNum}")]
        public void Delete(int id)
        {
        }
    }
}