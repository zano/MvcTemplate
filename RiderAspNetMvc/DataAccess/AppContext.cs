using System.Data.Entity;
using RiderAspNetMvc.Models;

namespace RiderAspNetMvc.DataAccess {
    public class AppDbContext : DbContext {
        public AppDbContext() : base("AppDb") {
               Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());        
        }

        public static AppDbContext Create() => new AppDbContext();

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
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