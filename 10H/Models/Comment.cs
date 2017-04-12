using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Comment
    {
        public int ID { get; set; }
        public virtual User User { get; set; }
        public int MusicID { get; set;}
        public string Comments { get; set; }
        public Decimal Note { get; set; }
    }
}
