using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AlbumsResponseVM
    {
            public List<Album> Albums { get; set; }
            public Album Album { get; set; }
            public List<Music> Musics { get; set; }
    }
}
