using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace _10H.Controllers
{
    public class HomeController : Controller
    {
        private _10HDBContext db = new _10HDBContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            List<Music> musics = db.Musics.OrderByDescending(a => a.Mark).ToList();
           
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Musics = musics
            };

            if(musics.Count == 1)
            {
                musicsResponseVM.Music1 = musics[0];
            } else if (musics.Count == 2)
            {
                musicsResponseVM.Music2 = musics[1];
                musicsResponseVM.Music1 = musics[0];
            }
            else if (musics.Count >= 3)
            {
                musicsResponseVM.Music3 = musics[2];
                musicsResponseVM.Music2 = musics[1];
                musicsResponseVM.Music1 = musics[0];
            }
            return View(musicsResponseVM);
        }
    }
}
