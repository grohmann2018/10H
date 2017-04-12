using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace _10H.Controllers
{
    public class MusicController : Controller
    {
        private _10HDBContext db = new _10HDBContext();

        // GET: Music
        public ActionResult Index()
        {
            var musics = db.Musics.Include(m => m.Album).ToList();

            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Musics = musics
            };

            return View(musicsResponseVM);
        }

        public ActionResult Play(int musicID)
        {
            var file = Server.MapPath("~/Content/Ressources/Musics/" + musicID.ToString() + ".mp3");
            return File(file, "audio/mp3");
        }

        [HttpPost]
        public PartialViewResult Search(MusicsResponseVM musicResponseVM)
        {
            Music m = musicResponseVM.Music1;
            List<Music> lm = db.Musics.ToList();

            if(m.Name != null)
                lm = lm.Where(c => c.Name.Equals(m.Name)).ToList();
            if(m.Artist != null)
                lm = lm.Where(c => c.Artist.Equals(m.Artist)).ToList();
            if (m.ReleaseDate != new DateTime(01, 01, 0001))
                lm = lm.Where(c => c.ReleaseDate.Equals(m.ReleaseDate)).ToList();
            if (m.Genre != null)
                lm = lm.Where(c => c.Genre.Equals(m.Genre)).ToList();

            return PartialView("_Musics", lm);
        }
    }
}