using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRAILES.Models
{
    public class Cabin
    {
        public int ID {get; set;}
        public string Name {get; set;}
        public Gender Gender {get; set;}
        [Display(Name="Bed Count")]
        public int BedCount {get; set;}
        [Display(Name="Beds Filled")]
        public int BedsFilled {get; set;} = 1;

        public ICollection<Student> Students {get; set;}
        public ICollection<FacStaff> Employees {get; set;}
    }
}