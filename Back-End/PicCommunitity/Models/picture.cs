using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class picture
    {
        [Key]
        public string p_id { get; set; }
        public int p_width { get; set; }
        public int p_height { get; set; }
        public string p_info { get; set; }
        public string p_url { get; set; }
        public string p_status { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public int comm_num { get; set; }
    }
}
