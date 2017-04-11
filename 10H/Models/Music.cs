using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Music
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public virtual Album Album { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int Number { get; set; }
        public int Thumbnail { get; set; }
    }
}
