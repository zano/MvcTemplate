using System.Data.Entity;
using RiderAspNetMvc.Models;

namespace RiderAspNetMvc.DataAccess {
    public class AppDbContext : DbContext {
        public AppDbContext() : base("AppDb") {
               Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());        
        }

        public static AppDbContext Create() => new AppDbContext();

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}