using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GotTalent_API.Models
{
    [Table("tb_stage_log")]
    public class StageLog
    {
        public int game_id { get; set; }
        public string action_type { get; set; }
        public double score { get; set; }
        public string file_loc { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public DateTime log_date { get; set; } 
    }
}