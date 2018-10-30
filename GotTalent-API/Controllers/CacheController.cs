using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CacheController : ControllerBase
    {
        private DataContext _context;

        public CacheController(DataContext context)
        {
            _context = context;
        }

        // GET api/cache/reload
        [HttpGet("reload")]
        public async Task<IActionResult> ReloadCache()
        {
            int gameResultCount = 0;

            var games = await _context.Game.Where(x => x.share_yn == "Y").ToListAsync();
            foreach (Game game in games)
            {
                var gameResults = await _context.GameResult.Where(x => x.game_id == game.game_id).ToListAsync();
                foreach (GameResult gameResult in gameResults)
                {
                    Console.WriteLine("cache loading game result for game_id : " + game.game_id);
                    RedisUtil.AddGameResultToRedis(gameResult);
                    gameResultCount++;
                }
            }
            return Ok(gameResultCount);
        }

        // GET api/cache/clear
        [HttpGet("clear")]
        public async Task<IActionResult> ClearCache()
        {
            // TODO: need to test
            // RedisUtil.ClearAll();

            return Ok();
        }
    }            
}