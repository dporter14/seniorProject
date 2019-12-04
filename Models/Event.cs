using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRAILES.Models
{
    public class Event
    {
        public int ID {get; set;}
        public string Name {get; set;}
        [Display(Name="Max Attendance")]
        public int MaxAttendance {get; set;}
        [Display(Name="Start Time")]
        public DateTime StartTime {get; set;}

        public ICollection<EventAttendance> Attendances {get; set;}
    }
}