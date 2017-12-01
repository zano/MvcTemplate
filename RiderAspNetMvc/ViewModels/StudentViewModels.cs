using System.Collections.Generic;
namespace RiderAspNetMvc.ViewModels {
    
    public class StudentListItem {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EnrollmentsCount { get; set; }
    }

    public class StudentEdit : StudentCreate {
    }

    public class StudentDetails : StudentEdit {
        public ICollection<EnrollmentListItem> Enrollments { get; set; }
    }

    public class StudentCreate {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class EnrollmentListItem {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Grade { get; set; }
    }
}