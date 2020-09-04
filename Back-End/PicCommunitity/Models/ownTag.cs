using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class ownTag
    {
        [Key,Column(Order =1)]
        public string p_id { get; set; }
        [Key, Column(Order = 2)]
        public string tag_name { get; set; }
    }
}
