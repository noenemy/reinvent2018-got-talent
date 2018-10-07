using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GotTalent_API.Models
{
    [Table("tb_cast")]
    public class Cast
    {
        [Key]
        public int cast_id { get; set; }
        public string title { get; set; }
        public string actor { get; set; }
        public string gender { get; set; } // Male, Female
        public string grade { get; set; } // A, B, C ...
        public string file_loc { get; set; }
        public string action_type { get; set; }
    }
}