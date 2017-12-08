using System.Data.Entity;
using RiderAspNetMvc.Models;
using System.Data.Entity.Infrastructure;
using System;

namespace RiderAspNetMvc.DataAccess
{
    public class DbContextFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext Create()
        {
            return AppDbContext.Create();            
        }
    }

    public class AppDbContext : DbContext
    {
        private AppDbContext() : base("AppDb") { }

        public static AppDbContext Create() => new AppDbContext();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Override the default precision of two decimals so that coordinates can be stored.
            //modelBuilder.Entity<Studio>().Property(s => s.Latitude).HasPrecision(18, 15);
            //modelBuilder.Entity<Studio>().Property(s => s.Longitude).HasPrecision(18, 15);

            // SQL Server's datetime2 has the same range and precision as .NET's DateTime
            modelBuilder.Properties<System.DateTime>().Configure(c => c.HasColumnType("datetime2"));

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }

}