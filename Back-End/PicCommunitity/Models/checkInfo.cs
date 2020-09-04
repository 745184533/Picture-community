using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class checkInfo
    {
        [Key]
        public string c_id { get; set; }
        public string p_id { get; set; }
        public string c_status { get; set; }
        public int pass_num { get; set; }
        public int fail_num { get; set; }
    }
}
