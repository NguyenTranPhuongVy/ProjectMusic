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
    public class AlbumsController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Albums
        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.User);
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }
        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name");
            return View();
        }
        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "album_id,album_name,album_datecreate,album_dateedit,album_view,album_love,album_active,album_bin,album_note,album_img,user_id")] Album album, HttpPostedFileBase imgAlbum, int [] musicAlbum)
        {
            Random random = new Random();
            ViewBag.Random = random.Next(0, 1000);
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            album.album_note = null;
            album.album_active = true;
            album.album_bin = false;
            album.album_datecreate = DateTime.Now;
            album.album_dateedit = DateTime.Now;
            album.album_love = 0;
            album.album_view = 0;
            album.user_id = user.user_id;

            db.Albums.Add(album);
            if (imgAlbum == null)
            {
                album.album_img = "album.jpg";
            }
            else
            {
                var fileimg = Path.GetFileName(imgAlbum.FileName);
                //Đưa tên ảnh vào file
                var pa = Path.Combine(Server.MapPath("~/Images/"), fileimg);
                imgAlbum.SaveAs(pa);
                album.album_img = ViewBag.Random + imgAlbum.FileName;
            }

            db.SaveChanges();

            foreach(var item in musicAlbum)
            {
                Music_Ablum music_Ablum = new Music_Ablum()
                {
                    music_id = item,
                    album_id = album.album_id,
                };
                db.Music_Ablum.Add(music_Ablum);
            }
            db.SaveChanges();
            
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", album.user_id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult AddPlaylist(int ? id, int [] musicAlbum)
        {
            foreach(var item in musicAlbum)
            {
                Music_Ablum music_Ablum = new Music_Ablum()
                {
                    music_id = id,
                    album_id = item,
                };
                db.Music_Ablum.Add(music_Ablum);
            }
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", album.user_id);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "album_id,album_name,album_datecreate,album_dateedit,album_view,album_love,album_active,album_bin,album_note,album_img,user_id")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", album.user_id);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
