using System;
using System.Collections.Generic;

namespace GotTalent_API.Models
{
    public class Ranking
    {
        public int total_rank { get; set; }
        public int game_id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public double total_score { get; set; }
        public string grade { get; set; }
        public string photoURL { get; set; }
    }
}