using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace TRAILES.Models
{
    public enum Gender
    {
        Male, Female, Other
    }
    public class Student : IdentityUser
    {
        [Display(Name = "First Name")]
        public string Fname { get; set; }
        [Display(Name = "Last Name")]
        public string Lname { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "Grade Level")]
        public int GradeLevel { get; set; }
        public bool Registered { get; set; } = false;
        [Display(Name = "Events Chosen")]
        public int priorityRemaining { get; set; } = 0;

        public int? CabinID { get; set; }
        public Cabin Cabin { get; set; }

        public ICollection<EventAttendance> EventAttendances { get; set; }
    }
}