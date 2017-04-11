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
            List<Music> musics = db.Musics.ToList();
           
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Musics = musics,
                Music1 = musics[0],
                Music2 = musics[1],
            };
            return View(musicsResponseVM);
        }
    }
}
