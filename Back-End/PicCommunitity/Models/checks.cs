using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class checks
    {
        [Key,Column(Order=1)]
        public string u_id { get; set; }
        [Key, Column(Order = 2)]
        public string c_id { get; set; }
        public DateTime c_time { get; set; }
    }
}
