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
        public async Task<ActionResult> Login([Bind(Include = "Email,Password")] User user, string returnUrl)
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
                return RedirectToAction("Index","Home");
            }
        
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View(user);
            //}

            //    Ceci ne comptabilise pas les échecs de connexion pour le verrouillage du compte
            //    Pour que les échecs de mot de passe déclenchent le verrouillage du compte, utilisez shouldLockout: true
            //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //User real_user = db.Users.Find(user.Email);
            //if (real_user == null)
            //{
            //    return View(user);
            //}
            //db.Users.Remove(user);
            //db.SaveChanges();

            //Redirect(returnUrl);
            //var result = await SignInManager.PasswordSignInAsync(user.Email, user.Password,false, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return Redirect(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Tentative de connexion non valide.");
            //        return View(user);
            //}
            //return RedirectToAction("../Home/Index");
        }






        //public async Task<ActionResult> Login(ModelBi model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    Ceci ne comptabilise pas les échecs de connexion pour le verrouillage du compte
        //    Pour que les échecs de mot de passe déclenchent le verrouillage du compte, utilisez shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Tentative de connexion non valide.");
        //            return View(model);
        //    }
        //}

        //public IHttpActionResult GetUser(int id)
        //{
        //    var user = db.Users.Where(c => c.ID.Equals(id));


        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

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
            //music.AlbumID = 1;

            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser { UserName = user.FirstName, Email = user.Email };
                var result = await UserManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
                    db.Users.Add(user);
                    db.SaveChanges();
                    if (Request.Cookies["userId"] != null)
                    {
                        Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
                    }

                    Response.Cookies["userId"]["id"] = user.ID.ToString();
                    Response.Cookies["userId"]["name"] = user.FirstName;
                    // Pour plus d'informations sur l'activation de la confirmation du compte et la réinitialisation du mot de passe, consultez http://go.microsoft.com/fwlink/?LinkID=320771
                    // Envoyer un message électronique avec ce lien
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmez votre compte", "Confirmez votre compte en cliquant <a href=\"" + callbackUrl + "\">ici</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
                
            }

            return View(user);
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
    }


}
