using System.Collections.Generic;

namespace RiderAspNetMvc.Models {
    public class Course {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Schedule Schedule { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
