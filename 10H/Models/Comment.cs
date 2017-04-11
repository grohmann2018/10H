using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Comment
    {
        public int ID { get; set; }
        public virtual User User { get; set; }
        public virtual Music Music { get; set;}
        public string Comments { get; set; }
        public Decimal Note { get; set; }
    }
}
