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
        public async Task<IActionResult> GetStageLog(int seqNum)
        {
            var value = await _context.StageLog.FirstOrDefaultAsync(x => x.SeqNum == seqNum);
            return Ok(value);
        }

        // POST api/stagelogs
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/stagelogs/5
        [HttpPut("{SeqNum}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/stagelogs/5
        [HttpDelete("{seqNum}")]
        public void Delete(int id)
        {
        }        
    }
}