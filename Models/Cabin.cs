using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRAILES.Models
{
    public class Cabin
    {
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name {get; set;}

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime AddDate {get; set;} = DateTime.Now;

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(10)]
        public string Gender {get; set;}

        [Range(1,30)]
        [Display(Name = "Bed Count")]
        public int BedCount {get; set;}

        [Display(Name = "Beds Filled")]
        [Range(0,30)]
        public int BedsFilled {get; set;} = 0;
    }
}