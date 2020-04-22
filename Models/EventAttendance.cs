using System;
using System.Collections.Generic;

namespace TRAILES.Models
{
    public class EventAttendance
    {
        public int EventAttendanceID { get; set; }
        public int Priority { get; set; }
        public bool? Assigned { get; set; }
        public double Weight { get; set; } = 0;

        public int EventID { get; set; }
        public Event Event { get; set; }
        public string StudentID { get; set; }
        public Student Student { get; set; }
    }
}