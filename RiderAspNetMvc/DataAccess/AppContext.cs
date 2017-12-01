using System.Data.Entity;
using RiderAspNetMvc.Models;

namespace RiderAspNetMvc.DataAccess {
    public class AppContext : DbContext {
        public static AppContext Create() => new AppContext();

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}