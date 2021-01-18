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
            List<Music> musics = db.Musics.Where(n => n.music_active == true && n.music_bin == false).OrderByDescending(n => n.music_datecreate).Take(6).ToList();
            return PartialView(musics);
        }

    }
}