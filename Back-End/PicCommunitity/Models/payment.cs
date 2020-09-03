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
        //(u_id varchar(8),
        //pay_time date,
        //money int,
        //source_typepicture varchar(4),
        //foreign key(u_id) references users(u_id),
        //primary key(u_id, pay_time)
        [Key, Column(Order = 1)]
        public string u_id { get; set; }

        [Key, Column(Order = 1)]
        public DateTime pay_time { get; set; }

        public int money { get; set; }
        
        public string source_typepicture { get; set; }
    }
}
