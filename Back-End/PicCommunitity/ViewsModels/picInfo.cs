using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.ViewsModels
{
    public class picInfo
    {
        public string picId { get; set; }
        public string picUrl { get; set; }
        public string publisherId { get; set; }
        public string publisherName { get; set; }
        public string info { get; set; }
        public int likeNum { get; set; }
        public int starNum { get; set; }
        public int commNum { get; set; }
    }
}
