using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRAILES.Models
{
    public enum Gender
    {
        M, F, O
    }
    public class Student
    {
        public int ID {get; set;}
        public string LastName {get; set;}
        public string FirstMidName {get; set;}
        public Gender Gender {get; set;}
        [Range(9,12)]
        public int GradeLevel {get; set;}
        public int priorityRemaining {get; set;} = 1;

        public int? CabinID {get; set;} 
        public Cabin Cabin {get; set;}

        public ICollection<EventAttendance> Attendances {get; set;}
    }
}