using System;
using System.Collections.Generic;

namespace TRAILES.Models
{
    public class FacStaff
    {
        public int ID {get; set;}
        public string LastName {get; set;}
        public string FirstMid {get; set;}
        public bool Admin {get; set;} = false;
        public int? CabinID {get; set;} 
        public Cabin Cabin {get; set;}  
    }
}