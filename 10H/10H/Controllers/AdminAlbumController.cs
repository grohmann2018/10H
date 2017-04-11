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
    public class AdminAlbumController : Controller
    {
        private _10HDBContext db = new _10HDBContext();

        // GET: AdminAlbum
        public ActionResult Index()
        {
            var albums = db.Albums.ToList();

            AlbumsResponseVM albumsResponseVM = new AlbumsResponseVM()
            {
                Albums = albums
            };

            return View(albumsResponseVM);
        }

        // Get: AdminAlbum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminAlbum/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Artist,ReleaseDate,Genre,Price")] Album Album, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                var path = Server.MapPath("~/Content/Ressources/Images/Albums/");
                int fileNumber = Directory.GetFiles(path).Length + 1;
                
                Album.Thumbnail = fileNumber;

                db.Albums.Add(Album);
                db.SaveChanges();
                
                string filename = Path.GetFileName(fileNumber.ToString() + ".png");
                ImageFile.SaveAs(Path.Combine(path, filename));

                return RedirectToAction("Index");
            }

            return View(Album);
        }

        // GET: AdminAlbum/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminAlbum/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AdminAlbum/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: AdminAlbums/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: AdminAlbum/Edit/id
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Artist,ReleaseDate,Genre,Price")] Album Album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Album).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(Album);
        }
    }
}