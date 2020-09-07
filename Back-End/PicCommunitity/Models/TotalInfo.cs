using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class TotalInfo
    {
        [Key]
        public int user_num { get; set; }

        public int blog_num { get; set; }
        public int pic_num { get; set; }
        public int check_num { get; set; }
        public int order_num { get; set; }

    }
}
