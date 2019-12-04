using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRAILES.Models
{
    public class FacStaff
    {
        public int ID {get; set;}
        [Display(Name="Last Name")]
        public string LastName {get; set;}
        [Display(Name="First Name")]
        public string FirstMid {get; set;}
        public bool Admin {get; set;} = false;
        public int? CabinID {get; set;} 
        public Cabin Cabin {get; set;}  
    }
}