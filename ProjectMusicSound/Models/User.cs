//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectMusicSound.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Musics = new HashSet<Music>();
            this.Pays = new HashSet<Pay>();
            this.Profiles = new HashSet<Profile>();
            this.Albums = new HashSet<Album>();
        }
    
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_img { get; set; }
        public string user_email { get; set; }
        public string user_pass { get; set; }
        public string user_token { get; set; }
        public Nullable<System.DateTime> user_datecreate { get; set; }
        public Nullable<System.DateTime> user_datelogin { get; set; }
        public Nullable<bool> user_active { get; set; }
        public Nullable<bool> user_option { get; set; }
        public Nullable<bool> user_bin { get; set; }
        public Nullable<int> role_id { get; set; }
        public string user_code { get; set; }
        public Nullable<int> user_point { get; set; }
        public Nullable<int> v_id { get; set; }
        public Nullable<System.DateTime> user_deadline { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Music> Musics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pay> Pays { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual Role Role { get; set; }
        public virtual Version Version { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Albums { get; set; }
    }
}
