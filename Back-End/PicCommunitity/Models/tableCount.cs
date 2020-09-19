using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class tableCount
    {
        [Key]
        public int tableKey { get; set; }
        public int blog { get; set; }
        public int picture { get; set; }
        public int users { get; set; }
    }
}
