using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.ViewsModels
{
    public class ProfileInfo
    {
        public int starNum { get; set; }
        public int followNum { get; set; }
        public int likeNum { get; set; }
        public int commentNum { get; set; }
    }

    public class ProfilePicture
    {
        public picture thatpicture { get; set; }

        public int like { get; set; }
        public int comment { get; set; }
        public int favorite { get; set; }
    }
}
