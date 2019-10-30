using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRAILES.Models
{
    public class Cabin
    {
        public int Id {get; set;}
        public string Name {get; set;}

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime AddDate {get; set;}
        public string Gender {get; set;}
        [Display(Name = "Bed Count")]
        public int BedCount {get; set;}
        [Display(Name = "Beds Filled")]
        public int BedsFilled {get; set;}
    }
}