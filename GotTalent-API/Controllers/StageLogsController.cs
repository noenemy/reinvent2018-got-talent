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
    public class StageLogsController : ControllerBase
    {
        private DataContext _context;

        public StageLogsController(DataContext context)
        {
            _context = context;
        }

        // GET api/stagelogs
        [HttpGet]
        public async Task<IActionResult> GetStageLogs()
        {
            var values = await _context.StageLog.ToListAsync();
            return Ok(values);
        }

        // GET api/stagelogs/5
        [HttpGet("{seqNum}")]
        public async Task<IActionResult> GetStageLog(int game_id)
        {
            var value = await _context.StageLog.Where(x => x.game_id == game_id).ToListAsync();
            return Ok(value);
        }

        // POST api/stagelogs
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/stagelogs/5
        [HttpPut("{game_id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/stagelogs/5
        [HttpDelete("{game_id}")]
        public void Delete(int id)
        {
        }        
    }
}