using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace _10H.Controllers
{
    public class OrderController : Controller
    {
        private _10HDBContext db = new _10HDBContext();

        // GET: Order
        public ActionResult Index()
        {
            if(Request.Cookies["userId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userId = int.Parse(Request.Cookies["userId"]["id"]);
            User user = db.Users.Find(userId);

            List<Order> orders = db.Orders.Where(i => i.UserID == userId).ToList();
            List<Music> musics = new List<Music>();
            Music music;
            for(int i =0; i< orders.Count; i++)
            {
                music = db.Musics.Find(orders[i].MusicID); 
                if(music != null)
                {
                    musics.Add(music);
                }
            }
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM
            {
                Musics = musics
            };

            return View(musicsResponseVM);
        }

       
        public ActionResult Buy(int id)
        {
            int userID = int.Parse(Request.Cookies["UserId"]["Id"]);
            User user = db.Users.Find(userID);
            if(user == null)
            {
                return HttpNotFound();
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            if(music.Price > user.Solde)
            {
                ModelState.AddModelError("", "Vous n'avez pas assez d'argent pour acheter cette musique");
                return RedirectToAction("Details", "AdminMusic", new { id = id });
            }
            Order order = new Order();
            order.UserID = userID;
            order.MusicID = id;
            order.Date = DateTime.Now;
            if (db.Orders.Where(i => i.UserID == userID && i.MusicID == id).FirstOrDefault() != null)
            {
                return RedirectToAction("AlreadyBought", "Order", new { id = id });
            }

            user.Solde -= music.Price;
            db.Entry(user).State = EntityState.Modified;

            db.Orders.Add(order);
            db.SaveChanges();
            

            return RedirectToAction("BuyConfirmation", "Order", new { id = id });
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
                Music1 = music
            };
            //db.Users.Remove(user);
            //db.SaveChanges();
            return View(musicsResponseVM);
        }

        public ActionResult AlreadyBought(int id)
        {
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            MusicsResponseVM musicsResponseVM = new MusicsResponseVM()
            {
                Music1 = music
            };

            return View(musicsResponseVM);
        }
    }
}