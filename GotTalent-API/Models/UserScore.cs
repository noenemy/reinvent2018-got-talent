using System;
using System.ComponentModel.DataAnnotations;

namespace GotTalent_API.Models
{
    public class UserScore
    {
        [Key]
        public int SeqNum { get; set; }
        public int UserNum { get; set; }
        public int TotalScore { get; set; }
        public DateTime CreateTime { get; set; }
    }
}