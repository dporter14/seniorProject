using System;
using System.Collections.Generic;

namespace TRAILES.Models
{
    public class EventAttendance
    {
        public int ID {get; set;}
        public int EventID {get; set;}
        public int StudentID {get; set;}
        public int Priority {get; set;}
        public bool? Assigned {get; set;} 

        public Event Event {get; set;}
        public Student Student {get; set;}
    }
}