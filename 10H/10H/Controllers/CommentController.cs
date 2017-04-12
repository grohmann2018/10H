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
    public class CommentController : Controller
    {

        private _10HDBContext db = new _10HDBContext();

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mark(Comment comment)
        {

            if (ModelState.IsValid)
            {
                //var newUser = new ApplicationUser { UserName = user.FirstName, Email = user.Email };
                // var result = await UserManager.CreateAsync(newUser, user.Password);
                // if (result.Succeeded)
                // {
                //   await SignInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
                int musicID = comment.MusicID;
                int userID = int.Parse(Request.Cookies["userId"]["id"]);
                User user = db.Users.Find(userID);
                comment.User = user;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Update", "Comment", new { musicID = musicID });
            }

            return RedirectToAction("Login", "User");
        }
        
        public ActionResult Update(int musicID)
        {
            if (ModelState.IsValid)
            {
                Music music = db.Musics.Find(musicID);
                List<Comment> comments =  db.Comments.Where(i => (i.MusicID == music.ID)).ToList();
                int count = 0;
                Decimal mark = 0;
                foreach(var comment in comments)
                {
                    if(comment.Note == -1)
                    {
                        comments.Remove(comment);
                        continue;
                    }
                    else
                    {
                        mark += comment.Note;
                        count++;
                    }
                }
                music.Mark = (Decimal)(mark / count);
                db.Entry(music).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("Index", "Home");
        }


        public ActionResult AddComment(int id)
        {
            Music music = db.Musics.Find(id);
            List<Comment> comments = db.Comments.Where(i => (i.MusicID == music.ID)).ToList();
            List<Comment> TMPcomments = new List<Comment>();
            if (comments != null)
            {
                for (int i = 0; i < comments.Count; i++)
                {
                    if (comments[i].Comments != null)
                        TMPcomments.Add(comments[i]);
                }
                //foreach(var comment in comments)
                //{
                //    if (comment.Comments == null)
                //        comments.Remove(comment);
                //}
            }


            CommentsResponseVM commentResponseVM = new CommentsResponseVM
            {
                Comments = TMPcomments,
                Music = music
            };

            return View(commentResponseVM);
        }


        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Note = -1;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
    }
}