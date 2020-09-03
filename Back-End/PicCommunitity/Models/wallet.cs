using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class wallet
    {

        [Key]
        public string u_id { get; set; }

        public int fund { get; set; }
        public int coin { get; set; }
        public int publish_num { get; set; }
        public int buy_num { get; set; }

    }
}
