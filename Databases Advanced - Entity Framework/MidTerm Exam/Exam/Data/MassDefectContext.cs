namespace Exam.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }


        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Anomaly> Anomalies { get; set; }
        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>().HasOptional(c => c.OriginPlanet).WithMany().HasForeignKey(c => c.OriginPlanetId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Anomaly>().HasOptional(c => c.TeleportPlanet).WithMany().HasForeignKey(c => c.TeleportPlanetId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Star>().HasOptional(c => c.SolarSystem).WithMany().HasForeignKey(c => c.SolarSystemId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>().HasMany(p => p.Victims).WithMany(c => c.Anomalies).Map(c =>
              {
                  c.MapLeftKey("PersonId");
                  c.MapRightKey("AnomalyId");
                  c.ToTable("AnomalyVictims");
              });
            base.OnModelCreating(modelBuilder);
        }
    }

}