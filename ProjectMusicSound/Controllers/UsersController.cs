using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Controllers
{
    public class UsersController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
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
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_name,user_img,user_email,user_pass,user_token,user_datecreate,user_datelogin,user_active,user_option,user_bin,role_id,user_code")] User user)
        {
            User us = db.Users.SingleOrDefault(n => n.user_email == user.user_email);
            if(us != null)
            {
                ViewBag.Checkemail = "Email đã tồn tại! Vui lòng nhập lại email.";
            }    
            else
            {
                db.Users.Add(user);
                user.user_img = "userImg.png";
                user.user_token = Guid.NewGuid().ToString();
                user.user_datecreate = DateTime.Now;
                user.user_datelogin = DateTime.Now;
                user.user_active = true;
                user.user_option = true;
                user.user_bin = false;
                user.role_id = 1;
                db.SaveChanges();
                User checkemail = db.Users.SingleOrDefault(n => n.user_email == user.user_email);
                return RedirectToAction("CreateCode", new { id = checkemail.user_id });
            }
            return View(user);
        }
        public RedirectResult CreateCode(int ? id)
        {
            User user = db.Users.Find(id);
            user.user_code = "MS-" + id;
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        // GET: Users/Edit/5
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
            return View(user);
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_name,user_img,user_email,user_pass,user_token,user_datecreate,user_datelogin,user_active,user_option,user_bin,role_id,user_code")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
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
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
