using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GotTalent_API.Models
{
    [Table("tb_game_rank_type")]
    public class RankingByType
    {
        [Key]
        public string game_id { get; set; }
        public string action_type { get; set; }
        public int type_rank { get; set; }       
    }
}