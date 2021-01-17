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
                Profile profile = new Profile()
                {
                    user_id = checkemail.user_id,
                    profile_point = 0
                };
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("CreateCode", new { id = checkemail.user_id });
            }
            return View(user);
        }
        public RedirectResult CreateCode(int ? id)
        {
            User user = db.Users.Find(id);
            user.user_code = "MS-" + id;
            db.SaveChanges();
            HttpCookie httpCookie = new HttpCookie("user_id", id.ToString());
            httpCookie.Expires.AddDays(10);
            Response.Cookies.Set(httpCookie);
            return Redirect("/Home/Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            String sEmail = f["user_email"].ToString();
            String sPass = f["user_pass"].ToString();
            User user = db.Users.Where(n => n.user_active == true && n.user_bin == false && n.role_id == 1).SingleOrDefault(n => n.user_email == sEmail && n.user_pass == sPass);
            if(user != null)
            {
                HttpCookie httpCookie = new HttpCookie("user_id", user.user_id.ToString());
                httpCookie.Expires.AddDays(10);
                Response.Cookies.Set(httpCookie);
                db.Users.Find(user.user_id).user_datelogin = DateTime.Now;
                db.Users.Find(user.user_id).user_token = Guid.NewGuid().ToString();
                db.SaveChanges();
                return Redirect("/");
            }
            else
            {
                ViewBag.CheckLogin = "Tài Khoản Không Tồn Tại";
            }
            return View(user);
        }

        public ActionResult LogOut()
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            httpCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Set(httpCookie);
            return RedirectToAction("Login");
        }
        public ActionResult Profile(int? id)
        {
            return View ();
        }    
        public ActionResult EditPass(int ? id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditPass(FormCollection f ,int ? id)
        {
            User user = db.Users.Find(id);
            String sOldPass = f["user_oldpass"].ToString();
            String sNewPass = f["user_newpass"].ToString();
            if(sOldPass != user.user_pass)
            {
                ViewBag.CheckOld = "Mật Khẩu Không Đúng";
            }
            else
            {
                if(sNewPass == user.user_pass)
                {
                    ViewBag.CheckNew = "Mật Khẩu Không Trùng Với Mật Khẩu Cũ";
                }
                else
                {
                    user.user_pass = sNewPass;
                    db.SaveChanges();
                    HttpCookie httpCookie = Request.Cookies["user_id"];
                    User users = db.Users.Find(int.Parse(httpCookie.Value.ToString()));
                    return Redirect("/Home/Index");
                }
            }
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
        //Cài Đặt Giao Diện
        public ActionResult Template()
        {
            return View();
        }

        //Thông Tin Cá Nhân
        [HttpPost]
        public ActionResult EditName(FormCollection f, int ? id)
        {
            var sName = f["user_name"].ToString();
            User user = db.Users.Find(id);

            user.user_name = sName;
            db.SaveChanges();
            return Redirect("/Users/Profile/" + id);
        }

        //Sai
        [HttpPost]
        public ActionResult EditAvatar(FormCollection f, int ? id, HttpPostedFileBase img)
        {
            User user = db.Users.Find(id);
            if (img != null)
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

            db.SaveChanges();
            return Redirect("/Users/Profile/" + id);
        }

        [HttpPost]
        public ActionResult EditPhone(FormCollection f, int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var sPhone = f["profile_phone"].ToString();
            Profile profile = db.Profiles.Find(id);

            profile.profile_phone = sPhone;
            db.SaveChanges();
            return Redirect("/Users/Profile/" + user.user_id);
        }

        [HttpPost]
        public ActionResult EditLastName(FormCollection f, int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var sLastName = f["profile_lastname"].ToString();
            var sName = f["profile_name"].ToString();
            Profile profile = db.Profiles.Find(id);

            profile.profile_lastname = sLastName;
            profile.profile_name = sName;
            db.SaveChanges();
            return Redirect("/Users/Profile/" + user.user_id);
        }

        [HttpPost]
        public ActionResult EditNote(FormCollection f, int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var sNote = f["profile_note"].ToString();
            Profile profile = db.Profiles.Find(id);

            profile.profile_note = sNote;
            db.SaveChanges();
            return Redirect("/Users/Profile/" + user.user_id);
        }

        [HttpPost]
        public ActionResult EditBirth(FormCollection f, int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var sBirth = f["profile_birth"].ToString();
            Profile profile = db.Profiles.Find(id);

            profile.profile_birth = DateTime.Parse(sBirth);
            db.SaveChanges();
            return Redirect("/Users/Profile/" + user.user_id);
        }

        [HttpPost]
        public ActionResult EditSex(FormCollection f, int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var sSex = f["sex_id"].ToString();
            Profile profile = db.Profiles.Find(id);

            profile.sex_id = Int32.Parse(sSex);
            db.SaveChanges();
            return Redirect("/Users/Profile/" + user.user_id);
        }

        [HttpPost]
        public ActionResult EditAddress(FormCollection f, int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var sAddress = f["profile_address"].ToString();
            Profile profile = db.Profiles.Find(id);

            profile.profile_address = sAddress;
            db.SaveChanges();
            return Redirect("/Users/Profile/" + user.user_id);
        }

        [HttpPost]
        public ActionResult EditFavorites(FormCollection f, int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var sFavorites = f["profile_favorite"].ToString();
            Profile profile = db.Profiles.Find(id);

            profile.profile_favorite = sFavorites;
            db.SaveChanges();
            return Redirect("/Users/Profile/" + user.user_id);
        }

    }
}
