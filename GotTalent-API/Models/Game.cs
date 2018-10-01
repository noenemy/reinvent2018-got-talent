using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GotTalent_API.Models
{
    [Table("tb_game")]
    public class Game
    {
        [Key]
        public int game_id { get; set; }
        public string name { get; set; }
        public string share_yn { get; set; } // Y/N
        public DateTime start_date { get; set; }
        public DateTime? end_date { get; set; }      
    }
}