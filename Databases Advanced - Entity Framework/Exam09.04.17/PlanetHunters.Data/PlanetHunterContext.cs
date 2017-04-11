namespace PlanetHunters.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PlanetHunterContext : DbContext
    {
        public PlanetHunterContext()
            : base("name=PlanetHunterContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PlanetHunterContext>());
        }

        public virtual DbSet<Star> Stars { get; set; }


        public virtual DbSet<Discovery> Discoveries { get; set; }

        public virtual DbSet<Astronomer> Astronomers { get; set; }

        public virtual DbSet<Telescope> Telescopes { get; set; }

        public virtual DbSet<StarSystem> StarSystems { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Astronomer>().HasMany(p => p.ObserverDiscoveries).WithMany(c => c.Observers).Map
                (c =>
                {
                    c.MapLeftKey("ObserverId");
                    c.MapRightKey("DiscoveryId");
                    c.ToTable("ObservedDiscoveries");

                });

            modelBuilder.Entity<Astronomer>().HasMany(p => p.PioneerDiscoveries).WithMany(c => c.Pioneers).Map
                (c =>
                {
                    c.MapLeftKey("PioneerId");
                    c.MapRightKey("DiscoveryId");
                    c.ToTable("PioneerDiscoveries");

                });

            base.OnModelCreating(modelBuilder);
        }
    }

}