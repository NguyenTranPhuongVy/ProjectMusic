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
    
    public partial class Album_Category
    {
        public int ac_id { get; set; }
        public Nullable<int> album_id { get; set; }
        public Nullable<int> category_id { get; set; }
    
        public virtual Album Album { get; set; }
        public virtual Category Category { get; set; }
    }
}