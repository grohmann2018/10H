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
    public class AdminMusicController : Controller
    {
        private _10HDBContext db = new _10HDBContext();

        // GET: AdminMusic
        public ActionResult Index()
        {
            var musics = db.Musics.Include(m => m.Album).ToList();

            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Musics = musics
            };

            return View(musicsResponseVM);
        }

        // Get: AdminMusic/Create
        public ActionResult Create()
        {
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Albums = db.Albums.ToList()
            };
            return View(musicsResponseVM);
        }

        // POST: AdminMusic/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //Eviter l'injection de script par onglet
        public ActionResult Create(MusicsResponseVM MusicVM, HttpPostedFileBase MusicFile)
        {
            if (ModelState.IsValid && MusicFile != null)
            {
                Music Music = MusicVM.Music1;

                var path = Server.MapPath("~/Content/Ressources/Musics/");
                int fileNumber = Directory.GetFiles(path).Length + 1;

                Music.Album = db.Albums.First();
                Music.Thumbnail = 1;
                Music.Number = fileNumber;
                Music.Mark = 0;
                Music.NumberOfComments = 0;

                db.Musics.Add(Music);
                db.SaveChanges();
                
                string filename = Path.GetFileName(fileNumber.ToString() + ".mp3");
                MusicFile.SaveAs(Path.Combine(path, filename));

                return RedirectToAction("Index");
            }

            return View(MusicVM);
        }

        // GET: AdminMusic/Delete/id
        public ActionResult Delete(int? id)
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
            db.Musics.Remove(music);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminMusic/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Musics.Find(id);
            db.Musics.Remove(music);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AdminMusic/Details/id
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

        // GET: AdminMusic/Edit/id
        public ActionResult Edit(int? id)
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

        // POST: AdminMusic/Edit/id
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Artist,ReleaseDate,Genre,Price,Duration,Number")] Music Music)
        {
            if (ModelState.IsValid)
            {
                //Music.AlbumID = 1;
                
                db.Entry(Music).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(Music);
        }

        public ActionResult Play(int musicID)
        {
            var file = Server.MapPath("~/Content/Ressources/Musics/" + musicID.ToString() + ".mp3");
            return File(file, "audio/mp3");
        }
    }
}