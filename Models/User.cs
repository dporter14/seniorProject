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

        // 13 == not a student
        [Range(9,13)]
        [Required]
        [Display(Name = "Grade Level")]
        public int GradeLevel {get; set;} 
        [Required]
        public bool IsAdmin {get; set;} = false;
        
        public int CabinId {get; set;} = 1;
        public Cabin Cabin {get; set;}
    }
}