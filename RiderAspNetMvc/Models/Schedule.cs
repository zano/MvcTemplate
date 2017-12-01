using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiderAspNetMvc.Models {
    [ComplexType]
    public class Schedule {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan Length => End - Start;
        public bool IsValid => Start.Date <= End.Date;
    }
}