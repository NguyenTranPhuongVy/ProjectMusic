using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Areas.Admin.Controllers
{
    public class UsersAdminController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Admin/UsersAdmin
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role).Where(n => n.user_bin == false && n.role_id == 1);
            return View(users.ToList());
        }

        //Xoá User
        public ActionResult Del(int ? id)
        {
            User user = db.Users.Find(id);
            user.user_bin = !user.user_bin;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteUsers()
        {
            var users = db.Users.Include(u => u.Role).Where(n => n.user_bin == true && n.role_id == 1);
            return View(users.ToList());
        }

        //Đổi Trạng Thái
        public JsonResult Active(int ? id)
        {
            User user = db.Users.Find(id);
            user.user_active = !user.user_active;
            db.SaveChanges();
            return Json(user,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Option(int? id)
        {
            User user = db.Users.Find(id);
            user.user_option = !user.user_option;
            db.SaveChanges();
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/UsersAdmin/Details/5
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

        // GET: Admin/UsersAdmin/Create
        public ActionResult Create()
        {
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "role_name");
            return View();
        }

        // POST: Admin/UsersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_name,user_img,user_email,user_pass,user_token,user_datecreate,user_datelogin,user_active,user_option,user_bin,role_id,user_code")] User user, HttpPostedFileBase img)
        {
            Random random = new Random();
            ViewBag.Random = random.Next(0, 1000);
            User us = db.Users.SingleOrDefault(n => n.user_email == user.user_email);
            var fileimg = "";
            var pa = "";
            if (us != null)
            {
                ViewBag.Checkemail = "Email đã tồn tại! Vui lòng nhập lại email.";
            }
            else
            {
                db.Users.Add(user);
                if (img == null)
                {
                    ViewBag.Checkimg = "Không Có Hình Ảnh";
                    user.user_img = "userImg.png";
                }
                else
                {
                    fileimg = Path.GetFileName(img.FileName);
                    //Đưa tên ảnh vào file
                    pa = Path.Combine(Server.MapPath("~/Images/User"), ViewBag.Random + fileimg);
                    img.SaveAs(pa);
                    user.user_img = ViewBag.Random + img.FileName;
                }
                user.user_token = Guid.NewGuid().ToString();
                user.user_datecreate = DateTime.Now;
                user.user_datelogin = DateTime.Now;
                db.SaveChanges();
                User checkemail = db.Users.SingleOrDefault(n => n.user_email == user.user_email);
                return RedirectToAction("CreateCode", new { id = checkemail.user_id });
            }
            return View(user);
        }
        public RedirectResult CreateCode(int? id)
        {
            User user = db.Users.Find(id);
            user.user_code = "MS-" + id;
            db.SaveChanges();
            return Redirect("/Admin/UsersAdmin/Index");
        }

        // GET: Admin/UsersAdmin/Edit/5
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
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "role_name", user.role_id);
            return View(user);
        }

        // POST: Admin/UsersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_name,user_img,user_email,user_pass,user_token,user_datecreate,user_datelogin,user_active,user_option,user_bin,role_id,user_code")] User user, HttpPostedFileBase img)
        {
            if(img != null)
            {
                var fileimg = Path.GetFileName(img.FileName);
                //Đưa tên ảnh vào file
                var pa = Path.Combine(Server.MapPath("~/Images/User"), fileimg);
                img.SaveAs(pa);
                user.user_img = img.FileName;
            }
            else
            {

            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "role_name", user.role_id);
            return Redirect("/Admin/UsersAdmin/Index");
        }

        // GET: Admin/UsersAdmin/Delete/5
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

        // POST: Admin/UsersAdmin/Delete/5
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
