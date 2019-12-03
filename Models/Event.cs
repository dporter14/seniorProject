using System;
using System.Collections.Generic;

namespace TRAILES.Models
{
    public class Event
    {
        public int ID {get; set;}
        public string Name {get; set;}
        public int MaxAttendance {get; set;}

        public ICollection<EventAttendance> Attendances {get; set;}
    }
}