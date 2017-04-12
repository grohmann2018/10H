using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using Models;
using ViewModels;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using _10H.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace _10H.Controllers
{
    public class UserController : Controller
    {
        private _10HDBContext db = new _10HDBContext();

        //User[] users = new User[]
        //{
        //    new User { ID = 1, FirstName = "Tomato Soup", Password = "Groceries"},
        //    new User { ID = 2, FirstName = "Yo-yo", Password = "Toys" }
        //};

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            List<User> users = db.Users.ToList();
            UsersResponseVM usersResponseVM = new UsersResponseVM()
            {
                Users = users
            };
            return View(usersResponseVM);
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = db.Users.ToList();

            return users;
        }

        public ActionResult GetUser(int id)
        {
            List<User> users = db.Users.ToList();
            
            User user = db.Users.Find(id);
            return View(user);
        }

        //GET : User/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //POST : User/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] User user, string returnUrl)
        {
            var newUser = db.Users.Where(i => (i.Email == user.Email) && (i.Password == user.Password)).FirstOrDefault();

            if (newUser == null)
            {
                return View("Index");
            }
            else
            {
                if (Request.Cookies["userId"] != null)
                {
                    Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
                }

                Response.Cookies["userId"]["id"] = newUser.ID.ToString();
                Response.Cookies["userId"]["name"] = newUser.FirstName;
                Response.Cookies["userId"]["roleId"] = newUser.RoleID.ToString();
                return RedirectToAction("Index","Home");
            }
        }



        // Get: User/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken] //Eviter l'injection de script par onglet
        public async Task<ActionResult> Register([Bind(Include = "FirstName,LastName,Email,Password,Adress")] User user ) //Bind : On récupère uniquement les attributs spécifiés
        {
            if (ModelState.IsValid)
            {
                User User = db.Users.Where(i => (i.Email == user.Email)).FirstOrDefault();
                if(User != null)
                {
                    ModelState.AddModelError("", "Un compte existe déjà avec cet email");
                    return View(user);
                }
                user.RoleID = 2;
                db.Users.Add(user);
                db.SaveChanges();
                if (Request.Cookies["userId"] != null)
                {
                    Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
                }
                Response.Cookies["userId"]["id"] = user.ID.ToString();
                Response.Cookies["userId"]["name"] = user.FirstName;
                Response.Cookies["userId"]["roleId"] = user.RoleID.ToString();
                return RedirectToAction("Index", "Home");                
            }

            return View(user);
        }

        public ActionResult LogOff()
        {
            if (Request.Cookies["userId"] != null)
            {
                Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
        }


        // GET: User/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: User/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Info(int id)
        {
            List<User> users = db.Users.ToList();
            foreach(User user in users)
            {
                if (user.ID.Equals(id))
                {
                    return View(user);
                }
            }
            return View(users);
        }




        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        // GET: User/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            UsersResponseVM usersResponseVM = new UsersResponseVM()
            {
                User = user
            };

            return View(usersResponseVM);
        }

        // POST: User/Edit/id
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsersResponseVM userVM)
        {
            if (ModelState.IsValid)
            {
                User user = userVM.User;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index","Home");
            }

            return View(userVM);
        }
    }


}
