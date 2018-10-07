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
    public class CastController : ControllerBase
    {
        private DataContext _context;

        public CastController(DataContext context)
        {
            _context = context;
        }

        // GET api/cast
        [HttpGet]
        public async Task<IActionResult> GetCastList()
        {
            var values = await _context.Cast.ToListAsync();
            return Ok(values);
        }

        // GET api/cast/5
        [HttpGet("{cast_id}")]
        public async Task<IActionResult> GetCast(int cast_id)
        {
            var value = await _context.Cast.FirstOrDefaultAsync(x => x.cast_id == cast_id);
            return Ok(value);
        }

        // POST api/cast
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/cast/5
        [HttpPut("{cast_id}")]
        public void Put(int cast_id, [FromBody] string value)
        {
        }

        // DELETE api/cast/5
        [HttpDelete("{cast_id}")]
        public void Delete(int cast_id)
        {
        }
    }            
}