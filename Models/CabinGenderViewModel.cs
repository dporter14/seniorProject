using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TRAILES.Models
{
    public class CabinGenderViewModel
    {
        public List<Cabin> Cabins { get; set; }
        public SelectList Genders { get; set; }
        public string CabinGender { get; set; }
        public string SearchString { get; set; }
    }
}