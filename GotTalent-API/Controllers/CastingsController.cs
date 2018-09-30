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
    public class CastingsController : ControllerBase
    {
        private DataContext _context;

        public CastingsController(DataContext context)
        {
            _context = context;
        }

        // GET api/castings
        [HttpGet]
        public async Task<IActionResult> GetCastings()
        {
            var values = await _context.Casting.ToListAsync();
            return Ok(values);
        }

        // GET api/castings/5
        [HttpGet("{cast_id}")]
        public async Task<IActionResult> GetCasting(int cast_id)
        {
            var value = await _context.Casting.FirstOrDefaultAsync(x => x.cast_id == cast_id);
            return Ok(value);
        }

        // POST api/castings
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/castings/5
        [HttpPut("{cast_id}")]
        public void Put(int cast_id, [FromBody] string value)
        {
        }

        // DELETE api/castings/5
        [HttpDelete("{cast_id}")]
        public void Delete(int cast_id)
        {
        }
    }            
}