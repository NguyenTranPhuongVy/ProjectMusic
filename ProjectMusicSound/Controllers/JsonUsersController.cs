using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Controllers
{
    public class JsonUsersController : Controller
    {
        MusicDataEntities db = new MusicDataEntities();
        // GET: JsonUsers
        public JsonResult MusicList(int ? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));
            List<Music> music = db.Musics.Where(n => n.music_active == true && n.music_bin == false && n.music_option == true && n.user_id == user.user_id).OrderByDescending(n => n.music_datecreate).ToList();
            List<MusicJson> listms = music.Select(n => new MusicJson 
            { 
                music_id = n.music_id,
                music_active = n.music_active,
                music_bin = n.music_bin,
                music_datecreate = n.music_datecreate.ToString(),
                music_dateedit = n.music_dateedit.ToString(),
                music_dowload = n.music_dowload,
                music_img = n.music_img,
                music_linkdow = n.music_linkdow,
                music_love = n.music_love,
                music_lyric = n.music_lyric,
                music_name = n.music_name,
                music_option = n.music_option,
                music_time = n.music_time,
                music_view = n.music_view,
                user_id = n.user_id
            }).ToList();
            return Json(listms ,JsonRequestBehavior.AllowGet);
        }

        public JsonResult MusicActive(int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));
            List<Music> music = db.Musics.Where(n => n.music_option == false && n.music_active == true && n.music_bin == false && n.user_id == user.user_id).OrderByDescending(n => n.music_datecreate).ToList();
            List<MusicJson> listms = music.Select(n => new MusicJson
            {
                music_id = n.music_id,
                music_active = n.music_active,
                music_bin = n.music_bin,
                music_datecreate = n.music_datecreate.ToString(),
                music_dateedit = n.music_dateedit.ToString(),
                music_dowload = n.music_dowload,
                music_img = n.music_img,
                music_linkdow = n.music_linkdow,
                music_love = n.music_love,
                music_lyric = n.music_lyric,
                music_name = n.music_name,
                music_option = n.music_option,
                music_time = n.music_time,
                music_view = n.music_view,
                user_id = n.user_id
            }).ToList();
            return Json(listms, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MusicShow()
        {
            List<Music> music = db.Musics.Where(n => n.music_active == true && n.music_option == true && n.music_bin == false && n.User.role_id == 2).OrderByDescending(n => n.music_datecreate).Take(6).ToList();
            List<MusicJson> list = music.Select(n => new MusicJson
            {
                music_id = n.music_id,
                music_active = n.music_active,
                music_bin = n.music_bin,
                music_datecreate = n.music_datecreate.ToString(),
                music_dateedit = n.music_dateedit.ToString(),
                music_dowload = n.music_dowload,
                music_img = n.music_img,
                music_linkdow = n.music_linkdow,
                music_love = n.music_love,
                music_lyric = n.music_lyric,
                music_name = n.music_name,
                music_option = n.music_option,
                music_time = n.music_time,
                music_view = n.music_view,
                user_id = n.user_id,
                listarrsing = Singers(n.music_id)
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Music100()
        {
            List<Music> music = db.Musics.Where(n => n.music_active == true && n.music_option == true && n.music_bin == false && n.User.role_id == 2).OrderByDescending(n => n.music_view).Take(100).ToList();
            List<MusicJson> list = music.Select(n => new MusicJson
            {
                music_id = n.music_id,
                music_active = n.music_active,
                music_bin = n.music_bin,
                music_datecreate = n.music_datecreate.ToString(),
                music_dateedit = n.music_dateedit.ToString(),
                music_dowload = n.music_dowload,
                music_img = n.music_img,
                music_linkdow = n.music_linkdow,
                music_love = n.music_love,
                music_lyric = n.music_lyric,
                music_name = n.music_name,
                music_option = n.music_option,
                music_time = n.music_time,
                music_view = n.music_view,
                user_id = n.user_id,
                listarrsing = Singers(n.music_id)
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public string[] Singers(int? id)
        {
            List<string> myList = new List<string>();
            List<Music_Singer> music_Singers = db.Music_Singer.Where(n => n.music_id == id).ToList();
            foreach (var item in music_Singers)
            {
                myList.Add(item.Singer.singer_name);
            }
            string[] arr = myList.ToArray();
            return arr;
        }

        public JsonResult NewAlbum()
        {
            List<Album> albums = db.Albums.Where(n => n.album_active == true && n.album_bin == false && n.User.role_id == 2).OrderByDescending(n => n.album_datecreate).Take(12).ToList();
            List<AlbumJson> list = albums.Select(n => new AlbumJson
            {
                album_active = n.album_active,
                album_bin = n.album_bin,
                album_datecreate = n.album_datecreate.ToString(),
                album_dateedit = n.album_dateedit.ToString(),
                album_id = n.album_id,
                album_img = n.album_img,
                album_love = n.album_love,
                album_name = n.album_name,
                album_note = n.album_note,
                album_view = n.album_view,
                user_id = n.user_id
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewAlbum()
        {
            List<Album> albums = db.Albums.Where(n => n.album_active == true && n.album_bin == false && n.User.role_id == 2).OrderByDescending(n => n.album_view).Take(12).ToList();
            List<AlbumJson> list = albums.Select(n => new AlbumJson
            {
                album_active = n.album_active,
                album_bin = n.album_bin,
                album_datecreate = n.album_datecreate.ToString(),
                album_dateedit = n.album_dateedit.ToString(),
                album_id = n.album_id,
                album_img = n.album_img,
                album_love = n.album_love,
                album_name = n.album_name,
                album_note = n.album_note,
                album_view = n.album_view,
                user_id = n.user_id
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}