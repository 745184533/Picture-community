using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class blog
    {
        [Key]
        public string b_id { get; set; }
        public DateTime b_date { get; set; }
        public string b_type { get; set; }
        public string b_text { get; set; }
    }
}
