using TRAILES.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TRAILES.Pages.Students
{
    public class CabinNamePageModel : PageModel
    {
        public SelectList CabinNameSL {get; set;}

        public void PopulateCabinsDropDownList(TRAILES.Data.AppDbContext _context,
            object selectedCabin = null)
            {
                var cabinsQuery = from c in _context.Cabins
                                    orderby c.Name
                                    select c;

                CabinNameSL = new SelectList(cabinsQuery.AsNoTracking(),
                                                "CabinID", "Name", selectedCabin);
            }
    }
}