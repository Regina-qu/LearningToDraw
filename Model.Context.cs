﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Рисовалка
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DRAWellEntities : DbContext
    {
        public DRAWellEntities()
            : base("name=DRAWellEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<ImageType> ImageType { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<UserImage> UserImage { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
