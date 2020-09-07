using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public interface IDBRepository
    {
        blog GetFirstBlog();
    }
}
