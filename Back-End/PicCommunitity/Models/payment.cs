using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class payment
    {
        
        [Key]
        public string pay_id { get; set; }
        public string u_id { get; set; }

        public DateTime pay_time { get; set; }

        public int coin { get; set; }
        public string type { get; set; }
    }
}
