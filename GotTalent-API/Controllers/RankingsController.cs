using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using GotTalent_API.Data;
using GotTalent_API.Models;
using GotTalent_API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GotTalent_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingsController : ControllerBase
    {
        private DataContext _context;
        IAmazonS3 S3Client { get; set; }

        public RankingsController(DataContext context, IAmazonS3 s3Client)
        {
            _context = context;
            this.S3Client = s3Client;
        }

        // GET api/rankings
        [HttpGet]
        public async Task<IActionResult> GetRankings()
        {
            string bucketName = "reinvent-gottalent";

            List<Ranking> rankingList = new List<Ranking>();
            var topRankings = RedisUtil.GetTopRankings(0, 4);
            foreach (var item in topRankings)
            {
                Ranking ranking = new Ranking();
                ranking.total_rank = item.total_rank;

                var game = await _context.Game.FirstOrDefaultAsync(x => x.game_id == item.game_id);
                ranking.game_id = item.game_id;
                ranking.name = game.name;

                var gameResult = await _context.GameResult.FirstOrDefaultAsync(x => x.game_id == item.game_id);
                ranking.total_score = gameResult.total_score;
                ranking.gender = gameResult.gender_result;
                ranking.age = gameResult.age_result;
                ranking.grade = gameResult.grade_result;

                var stageLog = await _context.StageLog.FirstOrDefaultAsync(x => x.game_id == item.game_id && x.action_type == "Profile");
                ranking.photoURL = S3Util.GetPresignedURL(this.S3Client, bucketName, stageLog.file_loc);

                rankingList.Add(ranking);
            }

            return Ok(rankingList);
        }

        // GET api/rankings/happy
        [HttpGet("{action_type}")]
        public async Task<IActionResult> GetRankingsByType(string action_type)
        {
            // var value = await _context.RankingByType.Select(x => x.action_type == action_type).ToListAsync();
            return Ok();
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