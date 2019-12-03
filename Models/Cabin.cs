using System;
using System.Collections.Generic;

namespace TRAILES.Models
{
    public class Cabin
    {
        public int ID {get; set;}
        public string Name {get; set;}
        public Gender Gender {get; set;}
        public int BedCount {get; set;}
        public int BedsFilled {get; set;} = 1;

        public ICollection<Student> Students {get; set;}
        public ICollection<FacStaff> Employees {get; set;}
    }
}