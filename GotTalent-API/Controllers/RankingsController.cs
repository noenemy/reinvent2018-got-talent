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
            var values = await _context.Ranking.ToListAsync();
            return Ok(values);
        }

        // GET api/rankings/5
        [HttpGet("{seqNum}")]
        public async Task<IActionResult> GetRanking(int seqNum)
        {
            var value = await _context.Ranking.FirstOrDefaultAsync(x => x.SeqNum == seqNum);
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