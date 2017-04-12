using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CommentsResponseVM
    {
        public List<Comment> Comments { get; set; }
        public Music Music { get; set; }
        public Album Album { get; set; }
    }
}
