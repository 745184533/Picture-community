using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class picComment
    {
        //(u_id varchar(8),
        //p_id varchar(8),
        //p_comment varchar(50),
        //likes int,
        //primary key(u_id, p_id),
        //foreign key(u_id)references users(u_id),
        //foreign key(p_id)references picture(p_id)
        //);
        [Key, Column(Order = 1)]
        public string u_id { get; set; }

        [Key, Column(Order = 1)]
        public string p_id { get; set; }

        public string p_comment { get; set; }
        public int likes { get; set; }

    }
}
