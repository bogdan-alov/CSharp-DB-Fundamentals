namespace Photographers
{

    using Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Linq;

    public class PhotographersContext : DbContext
    {
        public PhotographersContext()
            : base("name=PhotographersContext")
        {

        }

        public virtual DbSet<Photographer> Photographers { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<Picture> Pictures { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        // public virtual DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Tag>().Property(t => t.TagText).HasMaxLength(50).HasColumnName("TagName").IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("Tag") { IsUnique = true }));

            base.OnModelCreating(modelBuilder);
        }

    }
}

