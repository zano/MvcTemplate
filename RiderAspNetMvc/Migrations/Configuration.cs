using RiderAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace RiderAspNetMvc.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //            ContextKey = "RiderAspNetMvc.DataAccess.AppDbContext";
        }

        protected override void Seed(DataAccess.AppDbContext db)
        {
            var firstNames = new[] {
                "Adelia", "Alba", "Alden", "Alessandra", "Alfreda", "Alisa", "Allene", "Allene", "Amber", "Ardella", "Ardelle", "Aretha", "Asuncion", "Belia", "Billie", "Branda", "Breann", "Brigida", "Britany", "Britteny", "Brittney", "Candi", "Carly", "Celsa", "Charles", "Charlie", //"Christina", "Chung", "Cinda", "Claire", "Clemente", "Cliff", "Corey", "Corliss", "Cortez", "Cyndi", "Damien", "Damion", "Danita", "Davida", "Denese", "Denise", "Deonna", "Diann", "Digna", "Dorian", "Dot", "Drema", "Edgar", "Edith", "Elayne", "Elfreda", "Elicia", "Else", "Emilie", "Emmy", "Faviola", "Felisa", "Freida", "Frida", "Gabriel", "Gala", "Geneva", "Gertha", "Ginny", "Gwenda", "Gwenda", "Ha", "Hal", "Herb", "Herman", "Hyon", "Inocencia", "Jane", "Jeanelle", "Jenice", "Jenine", "Jetta", "Joe", "Joesph", "Johnathan", "Johnie", "Joi", "Joshua", "Julietta", "Kaci", "Kanisha", "Karoline", "Karoline", "Kati", "Katrina", "Kaylee", "Kenyatta", "Kerrie", "Klara", "Kristie", "Lamont", "Larue", "Lashanda", "Lashandra", "Laticia", "Lauretta", "Leandra", "Lenna", "Leonie", "Letisha", "Lili", "Linh", "Linsey", "Lorelei", "Louise", "Lucina", "Marcelene", "Marshall", "Maryanne", "Maud", "Mckenzie", "Meghann", "Minna", "Minna", "Miss", "Mitsuko", "Monroe", "Mose", "Muriel", "My", "Myrna", "Nada", "Nannette", "Nena", "Nga", "Nicol", "Nu", "Ona", "Onie", "Patience", "Patria", "Patricia", "Pinkie", "Porfirio", "Queenie", "Rana", "Raul", "Reed", "Richie", "Rita", "Rocio", "Romelia", "Romeo", "Ronnie", "Rosana", "Ruben", "Ryan", "Sanda", "Santa", "Sara", "Sara", "Shanae", "Shane", "Sharonda", "Sheena", "Shelia", "Shelton", "Shirlene", "Siobhan", "Siu", "Sixta", "Skye", "Slyvia", "Sophia", "Stacie", "Stefania", "Stormy", "Suzanna", "Tad", "Talisha", "Teodoro", "Terrie", "Theda", "Theo", "Thresa", "Tomi", "Tonita", "Tracie", "Trinh", "Trudi", "Tuan", "Un", "Verdie", "Verona", "Vita", "Viviana", "Wai", "Wendy", "Wilfred", "Wilhemina", "Willene", "Wynona", "Yadira", "Zonia", 
            };

            var lastNames = new[] {
                "Abalos", "Ader", "Adolphson", "Alderman", "Alverez", "Ammann", "Angles", "Anstett", "Aune", "Barge", "Beauvais", "Benningfield", "Bess", // "Blessing", "Boddy", "Boney", "Brace", "Bridge", "Brunetti", "Buckles", "Bulman", "Byrum", "Campuzano", "Canterbury", "Chavers", "Clinkscales", "Coney", "Connor", "Cowan", "Crosier", "Darland", "Desmond", "Dolphin", "Dudley", "Edney", "Finamore", "Flynt", "Fontanez", "Fore", "Fuell", "Galindo", "Gehrke", "Gildea", "Gosser", "Guercio", "Hamil", "Hansell", "Higdon", "Hoard", "Hochstetler", "Holliday", "Holmen", "Keen", "Kimura", "Kivett", "Klemp", "Lambros", "Lanahan", "Lebouef", "Lobb", "Lundy", "Manning", "Maresca", "Markovich", "Masiello", "Matherne", "Mccallister", "Mcgonigle", "Mudge", "Neely", "Nish", "Orcutt", "Paik", "Panek", "Pardon", "Pilkington", "Posner", "Putman", "Rand", "Rasnick", "Reich", "Schalk", "Seagraves", "Sedberry", "Shillings", "Shipman", "Siegler", "Snuggs", "Stanley", "Stowe", "Styers", "Sylva", "Tardy", "Thiede", "Tobias", "Vigliotti", "Whitcher", "Wilcher", "Wolcott", "Yokoyama",
            };

            var fnLength = firstNames.Length;
            var lnLength = lastNames.Length;
            var skip = lnLength * fnLength / 1000 - 1;

            var students = lastNames
                .SelectMany(last => firstNames.Select(first => new Student
                {
                    FirstName = first,
                    LastName = last
                }))
                .Where((s, i) => i % skip == 0)
                .Distinct()
                .ToArray();


            db.Students.AddOrUpdate(s => new { s.FirstName, s.LastName }, students);
            db.SaveChanges();


            var courseNames = new[] {
                Tuple.Create("Java", "J"),
                Tuple.Create("C#", "C"),
                Tuple.Create("PHP", "P"),
                Tuple.Create(".NET MVC", "N")
            };

            var levels = new[] {
                Tuple.Create("Introduction to {0}", "11"),
                Tuple.Create("Advanced {0}", "12"),
                Tuple.Create("{0} for Dummies", "01"),
                Tuple.Create("{0} Enterprise", "21"),
                Tuple.Create("Managing {0} Applications", "31")
            };

            var start = DateTime.Now - TimeSpan.FromDays(60);

            var courses = courseNames
                .SelectMany(c => levels
                .Select(l => new Course
                {
                    Name = string.Format(l.Item1, c.Item1),
                    Code = c.Item2 + l.Item2,
                    Schedule = new Schedule
                    {
                        Start = start += TimeSpan.FromDays(7),
                        End = start + TimeSpan.FromDays(21)
                    }
                }))
                .Where((s, i) => i % 2 == 0)
                .ToArray();

            db.Courses.AddOrUpdate(c => c.Code, courses);

            var enrollmentCount = -1;

            var enrollments = new List<Enrollment>();
            Course course;
            for (var studentIndex = 0; studentIndex < students.Length; studentIndex++)
            {

                if (studentIndex % 5 == 0) continue; // Some students are not enrolled

                var student = students[studentIndex];

                enrollmentCount++;
                int courseIndex = enrollmentCount % courses.Length;
                //                if (courseIndex == courses.Length - 1) continue; // Last course is empty            

                course = courses[courseIndex];
                //throw new Exception($"{courseIndex}/{courses.Length} {course.Name}");
                //throw new Exception($"{studentIndex}/{students.Length} {student.Name}");

                string debug
                    = $"{studentIndex}/{students?.Length} ({ student?.Name }), { nameof(student) } is null\n"
                    + $"{courseIndex}/{courses?.Length} ({ course?.Name }), { nameof(course) } is null\n";

                if (student == null) { throw new Exception(debug); }
                if (course == null) { throw new Exception(debug); }

                var e = new Enrollment { CourseId = course.Id, StudentId = student.Id };

                //throw new Exception($"{e?.Student?.Name} ({e?.Course?.Name})");
                //throw new Exception(enrollments?.Count().ToString());

                enrollments.Add(e);
            }
            //db.Enrollments.AddOrUpdate

        }


    }
}
