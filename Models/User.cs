using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRAILES.Models
{
    public class User 
    {
        public int UserId {get; set;}

        [StringLength(255, MinimumLength = 1)]
        [Required]
        [Display(Name = "First Name")]
        public string FName {get; set;}

        [StringLength(255, MinimumLength = 1)]
        [Required]
        [Display(Name = "Last Name")]
        public string LName {get; set;}

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(10)]
        public string Gender {get; set;}

        public bool IsAdmin {get; set;} = false;
        
        public int CabinId {get; set;}
        public Cabin Cabin {get; set;}
    }
}