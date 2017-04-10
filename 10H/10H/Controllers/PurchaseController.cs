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
        public ActionResult Index()
        {

            Music music = db.Musics.Find(1);
            if (music == null)
            {
                return HttpNotFound();
            }
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Music = music
            };
            //db.Users.Remove(user);
            //db.SaveChanges();
            return View(musicsResponseVM);
        }

        [HttpPost]
        public ActionResult Buy(int musicID)
        {
            int userID = int.Parse(Request.Cookies["UserId"]["Id"]);

            Order order = new Order();
            order.UserID = userID;
            order.MusicID = musicID;

            db.Orders.Add(order);
            db.SaveChanges();

           
            return View(order);
        }


        
        public ActionResult BuyConfirmation(int id)
        {
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Music = music
            };
            //db.Users.Remove(user);
            //db.SaveChanges();
            return View(musicsResponseVM);
        }


    }
}