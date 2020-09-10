using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.ViewsModels
{
    public class registerUser
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string userPassword { get; set; }
    }
}
