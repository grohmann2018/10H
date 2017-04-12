using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace _10H.Controllers
{
    public class PurchaseController : Controller
    {
        private _10HDBContext db = new _10HDBContext();
        // GET: Purchase

        [HttpPost]
        public ActionResult Index(string name)
        {

            Music music = db.Musics.Find(1);
            if (music == null)
            {
                return HttpNotFound();
            }
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Music1 = music
            };
            //db.Users.Remove(user);
            //db.SaveChanges();
            return View(musicsResponseVM);
        }

        


        
        


    }
}