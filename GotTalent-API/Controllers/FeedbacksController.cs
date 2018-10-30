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
    public class FeedbacksController : ControllerBase
    {
        private DataContext _context;

        public FeedbacksController(DataContext context)
        {
            _context = context;
        }

        // GET api/cast
        [HttpGet]
        public async Task<IActionResult> GetFeedbackList()
        {
            var values = await _context.Feedback.ToListAsync();
            return Ok(values);
        }

        // GET api/feedback/5
        [HttpGet("{feedback_id}")]
        public async Task<IActionResult> GetFeedback(int feedback_id)
        {
            var value = await _context.Feedback.FirstOrDefaultAsync(x => x.feedback_id == feedback_id);
            return Ok(value);
        }

        // GET api/feedback/pick?actionType=&grade=
        [HttpGet("pick")]
        public async Task<IActionResult> GetRandomFeedback(string actionType, string grade)
        {
            // get a feedback randomly
            int randomRecord = new Random().Next() % _context.Feedback.Where(x => x.action_type == actionType && x.grade == grade).Count();;
            var feedbackResult = _context.Feedback.Where(x => x.action_type == actionType && x.grade == grade).Skip(randomRecord).Take(1).First();
            
            return Ok(feedbackResult);
        }

        // POST api/feedback
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/feedback/5
        [HttpPut("{feedback_id}")]
        public void Put(int feedback_id, [FromBody] string value)
        {
        }

        // DELETE api/feedback/5
        [HttpDelete("{feedback_id}")]
        public void Delete(int feedback_id)
        {
        }
    }            
}