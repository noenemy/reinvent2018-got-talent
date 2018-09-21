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
    public class UserScoresController : ControllerBase
    {
        private DataContext _context;

        public UserScoresController(DataContext context)
        {
            _context = context;
        }

        // GET api/userscores
        [HttpGet]
        public async Task<IActionResult> GetUserScores()
        {
            var values = await _context.UserScore.ToListAsync();
            return Ok(values);
        }

        // GET api/userscores/5
        [HttpGet("{seqNum}")]
        public async Task<IActionResult> GetUserScore(int seqNum)
        {
            var value = await _context.UserScore.FirstOrDefaultAsync(x => x.SeqNum == seqNum);
            return Ok(value);
        }

        // POST api/userscores
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/userscores/5
        [HttpPut("{seqNum}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/userscores/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }    
}