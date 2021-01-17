﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MusicDataEntities : DbContext
    {
        public MusicDataEntities()
            : base("name=MusicDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<Music_Author> Music_Author { get; set; }
        public virtual DbSet<Music_Category> Music_Category { get; set; }
        public virtual DbSet<Music_Singer> Music_Singer { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Pay> Pays { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Singer> Singers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
    }
}
