﻿using Models;
using System;
using System.Collections.Generic;
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
            var musics = db.Musics.ToList();

            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Musics = musics
            };

            return View(musicsResponseVM);
        }

        // Get: AdminMusic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminMusic/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //Eviter l'injection de script par onglet
        public ActionResult Create([Bind(Include = "Name,Artist,ReleaseDate,Genre,Price,Duration,Number")] Music music) //Bind : On récupère uniquement les attributs spécifiés
        {
            music.AlbumID = 1;

            if (ModelState.IsValid)
            {
                db.Musics.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(music);
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

        public ActionResult Play(int musicID)
        {
            var file = Server.MapPath("~/Content/Ressources/Musics/" + musicID.ToString() + ".mp3");
            return File(file, "audio/mp3");
        }
    }
}