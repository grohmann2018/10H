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
using System.Diagnostics;

namespace _10H.Controllers
{
    public class AdminMusicController : Controller
    {
        private _10HDBContext db = new _10HDBContext();

        // GET: AdminMusic
        public ActionResult Index()
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                var musics = db.Musics.Include(m => m.Album).ToList();

                MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
                {
                    Musics = musics
                };

                return View(musicsResponseVM);
            }

            return RedirectToAction("Index", "Home");
        }

        // Get: AdminMusic/Create
        public ActionResult Create()
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
                {
                    Albums = db.Albums.ToList(),
                    AlbumTemplate = new SelectList(db.Albums, "ID", "Name", 1)
                };
                return View(musicsResponseVM);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: AdminMusic/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //Eviter l'injection de script par onglet
        public ActionResult Create(MusicsResponseVM MusicVM, HttpPostedFileBase MusicFile)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                if (ModelState.IsValid && MusicFile != null)
                {
                    Music Music = MusicVM.Music1;

                    var path = Server.MapPath("~/Content/Ressources/Musics/");
                    var path2 = Server.MapPath("~/Content/Ressources/CutMusics/");
                    int fileNumber = Directory.GetFiles(path).Length + 1;

                    Music.Album = db.Albums.Find(MusicVM.selectedAlbumID);
                    Music.Thumbnail = Music.Album.Thumbnail;
                    Music.Number = fileNumber;
                    Music.Mark = 0;
                    Music.NumberOfComments = 0;

                    db.Musics.Add(Music);
                    db.SaveChanges();

                    string filename = Path.GetFileName(fileNumber.ToString() + ".mp3");
                    MusicFile.SaveAs(Path.Combine(path, filename));

                    string ffPath = Server.MapPath("~/Content/Ressources/ffmpeg-20170411-f1d80bc-win64-static/bin/ffmpeg.exe");
                    string processString = "-t 20 -i " + Path.Combine(path, filename) + " " + Path.Combine(path2, filename);

                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo = new System.Diagnostics.ProcessStartInfo(ffPath, processString);
                    p.Start();
                    p.WaitForExit();

                    return RedirectToAction("Index");
                }

                return View(MusicVM);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: AdminMusic/Delete/id
        public ActionResult Delete(int? id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
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

            return RedirectToAction("Index", "Home");
        }

        // POST: AdminMusic/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                Music music = db.Musics.Find(id);
                db.Musics.Remove(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: AdminMusic/Details/id
        public ActionResult Details(int? id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
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

            return RedirectToAction("Index", "Home");
        }

        // GET: AdminMusic/Edit/id
        public ActionResult Edit(int? id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
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

                MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
                {
                    Albums = db.Albums.ToList(),
                    AlbumTemplate = new SelectList(db.Albums, "ID", "Name", 1),
                    Music1 = music,
                    selectedAlbumID = music.Album.ID
                };

                return View(musicsResponseVM);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: AdminMusic/Edit/id
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MusicsResponseVM MusicVM)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                if (ModelState.IsValid)
                {
                    Music Music = MusicVM.Music1;
                    Music.Album = db.Albums.Find(MusicVM.selectedAlbumID);
                    Music.Thumbnail = Music.Album.Thumbnail;

                    db.Entry(Music).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(MusicVM);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Play(int musicID)
        {
            var file = Server.MapPath("~/Content/Ressources/Musics/" + musicID.ToString() + ".mp3");
            return File(file, "audio/mp3");
        }
    }
}