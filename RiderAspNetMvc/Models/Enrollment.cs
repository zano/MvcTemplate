﻿namespace RiderAspNetMvc.Models {
    public class Enrollment {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}