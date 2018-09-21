using System;
using System.ComponentModel.DataAnnotations;

namespace GotTalent_API.Models
{
    public class StageLog
    {
        [Key]
        public int SeqNum { get; set; }
        public int UserNum { get; set; }
        public int ActionType { get; set; }
        public int Score { get; set; }
        public DateTime CreateTime { get; set; } 
    }
}