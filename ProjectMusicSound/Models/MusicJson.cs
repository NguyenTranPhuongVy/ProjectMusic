using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMusicSound.Models
{
    public class MusicJson
    {
        public int music_id { get; set; }
        public string music_name { get; set; }
        public string music_img { get; set; }
        public string music_lyric { get; set; }
        public string music_time { get; set; }
        public Nullable<int> music_view { get; set; }
        public Nullable<int> music_dowload { get; set; }
        public Nullable<int> music_love { get; set; }
        public Nullable<int> user_id { get; set; }
        public string music_linkdow { get; set; }
        public string music_datecreate { get; set; }
        public string music_dateedit { get; set; }
        public Nullable<bool> music_active { get; set; }
        public Nullable<bool> music_bin { get; set; }
        public Nullable<bool> music_option { get; set; }

        public virtual User User { get; set; }
    }
}