using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.ViewsModels
{
    public class OtherComment
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public picComment content { get; set; }
    }
}
