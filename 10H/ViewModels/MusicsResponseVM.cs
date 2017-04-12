using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class MusicsResponseVM
    {
        public List<Music> Musics { get; set; }
        public Music Music1 { get; set; }
        public Music Music2 { get; set; }
        public Music Music3 { get; set; }

        public List<Album> Albums { get; set; }
    }
}
