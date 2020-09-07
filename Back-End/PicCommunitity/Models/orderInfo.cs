using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class orderInfo
    {
        [Key]
        public string o_id { get; set; }
        public string o_type { get; set; }
        public string o_status { get; set; }
        public string o_description { get; set; }
        public int reward { get; set; }
    }
}
