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
    
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            this.Album_Category = new HashSet<Album_Category>();
            this.Music_Ablum = new HashSet<Music_Ablum>();
        }
    
        public int album_id { get; set; }
        public string album_name { get; set; }
        public Nullable<System.DateTime> album_datecreate { get; set; }
        public Nullable<System.DateTime> album_dateedit { get; set; }
        public Nullable<int> album_view { get; set; }
        public Nullable<int> album_love { get; set; }
        public Nullable<bool> album_active { get; set; }
        public Nullable<bool> album_bin { get; set; }
        public string album_note { get; set; }
        public string album_img { get; set; }
        public Nullable<int> user_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album_Category> Album_Category { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Music_Ablum> Music_Ablum { get; set; }
    }
}
