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
using StackExchange.Redis;

namespace GotTalent_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : ControllerBase
    {
        private DataContext _context;
        IAmazonS3 S3Client { get; set; }

        public GameResultsController(DataContext context, IAmazonS3 s3Client)
        {
            _context = context;
            this.S3Client = s3Client;
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

        // POST api/gameresults/5/calc
        [HttpGet("{game_id}/calc")]
        public async Task<IActionResult> CalcGameResult(int game_id)
        {
            string bucketName = "reinvent-gottalent";

            var stageLogs = await _context.StageLog.Where(x => x.game_id == game_id).ToListAsync();
            double totalScore = 0.0f;
            string genderResult = "";
            string gradeResult = "";
            List<string> signedURLs = new List<string>();
            int ageResult = 0;
            foreach (var stageLog in stageLogs)
            {
                if (stageLog.action_type == "Profile")
                {
                    genderResult = stageLog.gender;
                    ageResult = stageLog.age;
                }
                else
                {
                    totalScore += stageLog.score;
                    signedURLs.Add(S3Util.GetPresignedURL(this.S3Client, bucketName, stageLog.file_loc));
                }
            }

            // TODO : need to use Cache service for judgement
            if (totalScore < 160)
                gradeResult = "Extra";
            else if (160 <= totalScore && totalScore < 190)
                gradeResult = "Supporting";
            else
                gradeResult = "Leading";
            
            // casting randomly
            int randomRecord = new Random().Next() % _context.Cast.Where(x => x.gender == genderResult && x.grade == gradeResult).Count();;
            var castResult = _context.Cast.Where(x => x.gender == genderResult && x.grade == gradeResult).Skip(randomRecord).Take(1).First();
            string resultPageUrl = "TBD";

            // Database update
            GameResult newGameResult = new GameResult{
                game_id = game_id,
                result_page_url = resultPageUrl,
                total_score = totalScore,
                total_rank = 0,
                cast_result = castResult.cast_id,
                grade_result = gradeResult,
                gender_result = genderResult,
                age_result = ageResult 
            };

            Game game = await _context.Game.Where(x => x.game_id == game_id).FirstOrDefaultAsync();
            game.end_date = DateTime.Now;
            game.share_yn = "Y";

            var value = _context.GameResult.Add(newGameResult);
            await _context.SaveChangesAsync();

            RedisUtil.AddGameResultToRedis(newGameResult);
            newGameResult.total_rank = RedisUtil.GetGameRanking(newGameResult.game_id) + 1;

            return Ok(new {newGameResult, castResult.actor, castResult.title, signedURLs});
        }


        // PUT api/gameresults/5
        [HttpPut("{game_id}")]
        public void Put(int game_id, [FromBody] string value)
        {
        }

        // DELETE api/gameresults/5
        [HttpDelete("{game_id}")]
        public async Task<IActionResult> Delete(int game_id)
        {
            GameResult gameResult = new GameResult() { game_id = game_id };
            _context.GameResult.Remove(gameResult);
            var result = await _context.SaveChangesAsync();

            return Ok(result);
        }
    }            
}