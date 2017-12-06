using System.Data.Entity;
using RiderAspNetMvc.Models;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System;

namespace RiderAspNetMvc.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDb")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());
        }

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


        public static void Seed(AppDbContext db)
        {            
            var firstNames = new[] { "Alessandra", "Alfreda", "Alisa", "Allene", "Ardelle", "Belia", "Billie", "Branda", "Brigida", "Candi", "Carly", "Charles", "Cinda", "Clemente", "Corey", "Corliss", "Cortez", "Damien", "Davida", "Denise", "Deonna", "Digna", "Dorian", "Dot", "Drema", "Edgar", "Edith", "Emilie", "Faviola", "Felisa", "Frida", "Gabriel", "Gwenda", "Hal", "Hyon", "Jenine", "Joe", "Johnathan", "Johnie", "Joi", "Kaci", "Karoline", "Kati", "Katrina", "Kaylee", "Kenyatta", "Kerrie", "Klara", "Lashandra", "Laticia", "Lili", "Lorelei", "Louise", "Lucina", "Marcelene", "Marshall", "Mckenzie", "Meghann", "Minna", "Miss", "Mitsuko", "Muriel", "Myrna", "Nada", "Nannette", "Nicol", "Nu", "Ona", "Onie", "Patricia", "Pinkie", "Queenie", "Reed", "Richie", "Romelia", "Romeo", "Ronnie", "Sara", "Shane", "Sharonda", "Shelia", "Siobhan", "Siu", "Sixta", "Sophia", "Stormy", "Talisha", "Teodoro", "Terrie", "Tomi", "Tonita", "Tracie", "Verdie", "Viviana", "Wendy", "Wilhemina", "Willene", "Wynona", "Yadira", "Zonia", "Denese", "Jetta", "Monroe", "Claire", "Maud", "Gwenda", "Allene", "Trudi", "Verona", "Lashanda", "Rocio", "Emmy", "Geneva", "Celsa", "Jeanelle", "Lenna", "Aretha", "Ruben", "Theo", "Slyvia", "Vita", "Britteny", "Wai", "Maryanne", "Ha", "Wilfred", "Leandra", "Thresa", "Elicia", "Freida", "Herb", "Stacie", "Amber", "Minna", "Patria", "Larue", "Rana", "Britany", "Christina", "My", "Ardella", "Cliff", "Shirlene", "Porfirio", "Herman", "Leonie", "Sheena", "Lamont", "Patience", "Suzanna", "Linsey", "Asuncion", "Julietta", "Diann", "Joesph", "Joshua", "Gala", "Charlie", "Karoline", "Shelton", "Alden", "Tad", "Jane", "Breann", "Trinh", "Cyndi", "Inocencia", "Adelia", "Rosana", "Alba", "Ryan", "Sara", "Letisha", "Theda", "Stefania", "Tuan", "Danita", "Un", "Ginny", "Else", "Sanda", "Skye", "Brittney", "Raul", "Jenice", "Lauretta", "Kanisha", "Gertha", "Mose", "Rita", "Shanae", "Chung", "Damion", "Nena", "Linh", "Kristie", "Santa", "Elayne", "Nga", "Elfreda" };

            var lastNames = new[] { "Galindo", "Paik", "Ammann", "Hamil", "Chavers", "Desmond", "Stowe", "Neely", "Markovich", "Benningfield", "Holmen", "Mcgonigle", "Seagraves", "Fore", "Putman", "Reich", "Lanahan", "Bess", "Styers", "Edney", "Byrum", "Barge", "Canterbury", "Siegler", "Brace", "Finamore", "Thiede", "Yokoyama", "Holliday", "Pilkington", "Orcutt", "Tardy", "Rand", "Kimura", "Flynt", "Cowan", "Crosier", "Manning", "Maresca", "Darland", "Bridge", "Aune", "Sylva", "Bulman", "Lobb", "Whitcher", "Nish", "Abalos", "Fontanez", "Alderman", "Snuggs", "Adolphson", "Pardon", "Dudley", "Anstett", "Boney", "Stanley", "Gosser", "Hochstetler", "Sedberry", "Campuzano", "Vigliotti", "Gildea", "Mudge", "Brunetti", "Angles", "Alverez", "Tobias", "Lundy", "Kivett", "Masiello", "Lambros", "Gehrke", "Clinkscales", "Beauvais", "Keen", "Mccallister", "Boddy", "Guercio", "Wilcher", "Shipman", "Higdon", "Posner", "Wolcott", "Hoard", "Buckles", "Connor", "Fuell", "Shillings", "Panek", "Dolphin", "Blessing", "Ader", "Coney", "Klemp", "Matherne", "Rasnick", "Hansell", "Lebouef", "Schalk" };

            var students = firstNames
                .SelectMany(f => lastNames
                .Select(l => new Student { FirstName = f, LastName = l }))
                .ToArray();

            db.Students.AddOrUpdate(s => new { s.FirstName, s.LastName }, students);

            var courseNames = new[] {
                Tuple.Create("Java", "J"),
                Tuple.Create("C#", "C"),
                Tuple.Create("PHP", "P"),
                Tuple.Create(".NET MVC", "N")
            };

            var levels = new[] {
                Tuple.Create("Introduction to {0} ", "11"),
                Tuple.Create("Advanced {0}", "12"),
                Tuple.Create("{0} for Dummies", "01")
            };

            var start = DateTime.Now - TimeSpan.FromDays(60);

            var courses = courseNames
                .SelectMany(c => levels
                .Select(l => new Course {
                    Name = string.Format(l.Item1, c.Item2),
                    Code = l.Item2 + c.Item2,
                    Schedule = new Schedule {
                        Start = start += TimeSpan.FromDays(7),
                        End = start + TimeSpan.FromDays(21)
                    }
                }))
                .ToArray();

            db.Courses.AddOrUpdate(c => c.Code, courses);
        }

        

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }

}