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
    public class MusicsAdminController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Admin/MusicsAdmin
        public ActionResult Index()
        {
            return View(db.Musics.Where(n => n.music_bin == false && n.User.role_id == 2).ToList());
        }

        public ActionResult MusicUser()
        {
            var musics = db.Musics.Include(m => m.User);
            return View(musics.Where(n => n.music_bin == false && n.User.role_id == 1).ToList());
        }

        //Đổi Trạng Thái
        public JsonResult Active(int? id)
        {
            Music music = db.Musics.Find(id);
            music.music_active = !music.music_active;
            db.SaveChanges();
            return Json(music, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Option(int? id)
        {
            Music music = db.Musics.Find(id);
            music.music_option = !music.music_option;
            db.SaveChanges();
            return Json(music, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelMusic()
        {
            List<Music> music = db.Musics.Where(n => n.music_bin == true).ToList();
            return View(music);
        }

        public ActionResult MusicDelete(int? id)
        {
            Music music = db.Musics.Find(id);
            music.music_bin = !music.music_bin;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/MusicsAdmin/Details/5
        public ActionResult Details(int? id)
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
            return View(music);
        }

        // GET: Admin/MusicsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name");
            return View();
        }

        // POST: Admin/MusicsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "music_id,music_name,music_img,music_lyric,music_time,music_view,music_dowload,music_love,user_id,music_linkdow,music_datecreate,music_dateedit,music_active,music_bin,music_option")] Music music, int[] category, int[] singer, int[] album, int[] author, HttpPostedFileBase filemp3, HttpPostedFileBase img)
        {

            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var fileimg = Path.GetFileName(img.FileName);
            //Đưa tên ảnh vào file
            var pa = Path.Combine(Server.MapPath("~/Images/"), fileimg);
            img.SaveAs(pa);

            var mp3 = Path.GetFileName(filemp3.FileName);
            var pathmp3 = Path.Combine(Server.MapPath("~/Content/LinkMusic/"), mp3);

            if (filemp3 == null)
            {
                return View();
            }
            else if (System.IO.File.Exists(pathmp3))
            {
                ViewBag.Img = "File had exists";
            }
            else
            {
                filemp3.SaveAs(pathmp3);
            }
            music.music_linkdow = filemp3.FileName;
            music.music_img = img.FileName;
            music.music_datecreate = DateTime.Now;
            music.music_dateedit = DateTime.Now;
            music.music_bin = false;
            music.music_love = 0;
            music.music_view = 0;
            music.music_dowload = 0;
            music.user_id = user.user_id;

            db.Musics.Add(music);
            db.SaveChanges();

            foreach (var item in category)
            {
                Music_Category music_Category = new Music_Category()
                {
                    music_id = music.music_id,
                    category_id = item
                };
                db.Music_Category.Add(music_Category);
            }

            foreach (var item in singer)
            {
                Music_Singer music_Singer = new Music_Singer()
                {
                    music_id = music.music_id,
                    singer_id = item
                };
                db.Music_Singer.Add(music_Singer);
            }  
            
            if(album != null)
            {
                foreach (var item in album)
                {
                    Music_Ablum music_Ablum = new Music_Ablum()
                    {
                        music_id = music.music_id,
                        album_id = item
                    };
                    db.Music_Ablum.Add(music_Ablum);
                }
            }

            if(author != null)
            {
                foreach (var item in author)
                {
                    Music_Author music_Author = new Music_Author()
                    {
                        music_id = music.music_id,
                        author_id = item
                    };
                    db.Music_Author.Add(music_Author);
                }
            }

            db.SaveChanges();

            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", music.user_id);
            return View(music);
        }

        // GET: Admin/MusicsAdmin/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", music.user_id);
            return View(music);
        }

        // POST: Admin/MusicsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "music_id,music_name,music_img,music_lyric,music_time,music_view,music_dowload,music_love,user_id,music_linkdow,music_datecreate,music_dateedit,music_active,music_bin,music_option")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", music.user_id);
            return View(music);
        }

        // GET: Admin/MusicsAdmin/Delete/5
        public ActionResult Delete(int? id)
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
            return View(music);
        }

        // POST: Admin/MusicsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Musics.Find(id);
            db.Musics.Remove(music);
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
