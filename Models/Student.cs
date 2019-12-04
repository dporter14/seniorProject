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
        [Display(Name="Last Name")]
        public string LastName {get; set;}
        [Display(Name="First Name")]
        public string FirstMidName {get; set;}
        public Gender Gender {get; set;}
        [Display(Name="Grade Level")]
        [Range(9,12)]
        public int GradeLevel {get; set;}
        [Display(Name="Next Priority")]
        public int priorityRemaining {get; set;} = 1;

        public int? CabinID {get; set;} 
        public Cabin Cabin {get; set;}

        public ICollection<EventAttendance> Attendances {get; set;}
    }
}