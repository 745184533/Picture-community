using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class users
    {

        [Key]
        public string u_id { get; set; }

        public string u_password { get; set; }
        public string u_name { get; set; }
        public string u_type { get; set; }
        public string u_status { get; set; }
        public DateTime create_time { get; set; }
    }
}
