using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GotTalent_API.Models
{
    [Table("tb_feedback")]
    public class Feedback
    {
        [Key]
        public int feedback_id { get; set; }
        public string judge_gender { get; set; } // Male, Female
        public string action_type { get; set; }
        public string grade { get; set; }
        public string feedback { get; set; }
    }
}