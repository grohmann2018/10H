using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _10H.Controllers
{
    public class PurchaseController : Controller
    {
        private _10HDBContext db = new _10HDBContext();
        // GET: Purchase
        public ActionResult Index(int? id)
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
            //db.Users.Remove(user);
            //db.SaveChanges();
            return View(music);
        }

        
        
    }
}