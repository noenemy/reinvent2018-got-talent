using System;
using System.Collections.Generic;
using GotTalent_API.Models;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace GotTalent_API.Utils
{
    public class RedisUtil
    {
        static string REDIS_SERVERNAME = "localhost";
        static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(REDIS_SERVERNAME);

        public static void AddGameResultToRedis(GameResult gameResult)
        {
            IDatabase db = redis.GetDatabase();

            var gameResults = new RedisDictionary<int, GameResult>("gameResults");
            gameResults.Add(gameResult.game_id, gameResult);

            db.SortedSetIncrement("leaderboard", gameResult.game_id, gameResult.total_score);
        }

        public static void AddGameResultListToRedis(List<GameResult> gameResultList)
        {
            IDatabase db = redis.GetDatabase();

            var gameResults = new RedisDictionary<int, GameResult>("gameResults");
            foreach (GameResult item in gameResultList)
            {
                gameResults.Add(item.game_id, item);
                db.SortedSetIncrement("leaderboard", item.game_id, item.total_score);
            }
        }

        public static int GetGameRanking(int game_id)
        {
            int result = 0;
            IDatabase db = redis.GetDatabase();

            long? rank = db.SortedSetRank("leaderboard", game_id, Order.Descending);

            if (rank.HasValue)
                result = Convert.ToInt32(rank.Value);
            
            return result;
        }

        public static List<GameResult> GetTopRankings(int start, int stop)
        {
           IDatabase db = redis.GetDatabase();

           SortedSetEntry[] list = db.SortedSetRangeByRankWithScores("leaderboard", start, stop, Order.Descending);

           var gameResults = new RedisDictionary<int, GameResult>("gameResults");
            List<GameResult> gameResultList = new List<GameResult>();
           for (int i=0; i < list.Length; i++)
           {
               Console.WriteLine(list[i].Element + ":" + list[i].Score);
               GameResult item = gameResults[Convert.ToInt32(list[i].Element)];
               item.total_rank = i + 1;
               gameResultList.Add(item);
           }
           return gameResultList;
        }

        public static void ClearAll()
        {
            var server = redis.GetServer(REDIS_SERVERNAME);
            server.FlushAllDatabases();
        }
    }
}