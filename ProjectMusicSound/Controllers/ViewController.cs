using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Controllers
{
    public class ViewController : Controller
    {
        MusicDataEntities db = new MusicDataEntities();
        // GET: View
        public PartialViewResult Validation()
        {
            return PartialView();
        }

        public PartialViewResult Menu()
        {
            return PartialView();
        }

        public PartialViewResult NewHit()
        {
            List<Music> musics = db.Musics.Where(n => n.music_active == true && n.music_bin == false && n.User.role_id == 2).OrderByDescending(n => n.music_datecreate).Take(6).ToList();
            return PartialView(musics);
        }

        public PartialViewResult NewAlbum()
        {
            List<Album> albums = db.Albums.Where(n => n.album_active == true && n.album_bin == false && n.User.role_id == 2).OrderByDescending(n => n.album_datecreate).Take(12).ToList();
            return PartialView(albums);
        }

        public PartialViewResult AlbumView()
        {
            List<Album> albums = db.Albums.Where(n => n.album_active == true && n.album_bin == false && n.User.role_id == 2).OrderByDescending(n => n.album_view).Take(12).ToList();
            return PartialView(albums);
        }
    }
}