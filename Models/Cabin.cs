using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRAILES.Models
{
    public class Cabin
    {
        public int CabinID { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "Bed Count")]
        public int BedCount { get; set; } = 9;
        [Display(Name = "Beds Taken")]
        public int BedsRegistered { get; set; } = 0;
        public string Chapperone { get; set; } = "";

        public ICollection<Student> Students;
    }
}