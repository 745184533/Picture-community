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
        
        [Key, Column(Order = 1)]
        public string u_id { get; set; }

        [Key, Column(Order = 2)]
        public DateTime pay_time { get; set; }

        public int money { get; set; }
        
        public string source_typepicture { get; set; }
    }
}
