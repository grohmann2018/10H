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
        //Request.Cookies["userId"]["id"]
        // GET: AdminAlbum
        public ActionResult Index()
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                var albums = db.Albums.ToList();

                AlbumsResponseVM albumsResponseVM = new AlbumsResponseVM()
                {
                    Albums = albums
                };

                return View(albumsResponseVM);
            }

            return RedirectToAction("Index", "Home");
        }

        // Get: AdminAlbum/Create
        public ActionResult Create()
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: AdminAlbum/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Artist,ReleaseDate,Genre,Price")] Album Album, HttpPostedFileBase ImageFile)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
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

            return RedirectToAction("Index", "Home");
        }

        // GET: AdminAlbum/Delete/id
        public ActionResult Delete(int? id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
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

            return RedirectToAction("Index", "Home");
        }

        // POST: AdminAlbum/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                Album album = db.Albums.Find(id);
                db.Albums.Remove(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: AdminAlbum/Details/id
        public ActionResult Details(int? id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
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

                AlbumsResponseVM albumResponseVM = new AlbumsResponseVM()
                {
                    Album = album,
                    Musics = db.Musics.Where(a => a.Album.ID == album.ID).ToList()
                };
                return View(albumResponseVM);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: AdminAlbums/Edit/id
        public ActionResult Edit(int? id)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
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

            return RedirectToAction("Index", "Home");
        }

        // POST: AdminAlbum/Edit/id
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Artist,ReleaseDate,Genre,Price")] Album Album)
        {
            if ((Request.Cookies["userId"]["roleId"]) == "1")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(Album).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(Album);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}