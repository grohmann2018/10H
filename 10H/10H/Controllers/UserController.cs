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
using System.Web.Http;

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


        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = db.Users.ToList();

            return users;
        }

        //public IHttpActionResult GetUser(int id)
        //{
        //    var user = db.Users.Where(c => c.ID.Equals(id));

           
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}
    }

     
}
