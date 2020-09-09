using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace picCommunity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(10,ErrorMessage ="MaxLength is 10")]
        public string u_name { get; set; }
        [Required]
        public string u_password { get; set; }
        [Required]
        [Compare("u_password",ErrorMessage ="两次输入密码不同")]
        public string confirm_u_password { get; set; }
        [Required]
        public string u_type { get; set; }
    }
}
