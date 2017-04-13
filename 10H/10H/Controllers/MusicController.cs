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
            if (Request.Cookies["UserId"] != null)
            {
                var musics = db.Musics.Include(m => m.Album).ToList();
                int userid = Int32.Parse(Request.Cookies["UserId"]["id"]);
                var orders = db.Orders.Where(u => u.UserID == userid).Select(u => u.MusicID).ToList();

                MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
                {
                    Musics = musics,
                    MusicOrderedIds = orders
                };

                return View(musicsResponseVM);
            }

            return RedirectToAction("Login", "User");
        }

        // GET: Music/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        public ActionResult Play(int musicID, string path)
        {
            var file = Server.MapPath("~/Content/Ressources/"+path+"/" + musicID.ToString() + ".mp3");
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

            int userid = Int32.Parse(Request.Cookies["UserId"]["id"]);
            var orders = db.Orders.Where(u => u.UserID == userid).Select(u => u.MusicID).ToList();

            MusicsResponseVM vm = new MusicsResponseVM();
            vm.Musics = lm;
            vm.MusicOrderedIds = orders;

            return PartialView("_Musics", vm);
        }
    }
}