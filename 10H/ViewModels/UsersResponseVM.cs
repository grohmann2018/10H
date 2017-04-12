using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels
{
    public class UsersResponseVM
    {
        public List<User> Users { get; set; }
        public User User { get; set; }
    }
}
