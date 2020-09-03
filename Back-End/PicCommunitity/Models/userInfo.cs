using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class userInfo
    {

        [Key]
        public string u_id { get; set; }

        public string u_name { get; set; }
        public DateTime birthday { get; set; }
        public string phone_number { get; set; }
        public string mail { get; set; }
        public string message { get; set; }
    }
}
