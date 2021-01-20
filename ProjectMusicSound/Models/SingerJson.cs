using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMusicSound.Models
{
    public class SingerJson
    {
        public int singer_id { get; set; }
        public string singer_name { get; set; }
        public Nullable<bool> singer_active { get; set; }
        public Nullable<bool> singer_bin { get; set; }
        public string singer_note { get; set; }
        public string singer_img { get; set; }
    }
}