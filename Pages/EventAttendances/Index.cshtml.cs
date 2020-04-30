using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;
using Microsoft.AspNetCore.Authorization;

namespace TRAILES.Pages.EventAttendances
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class IndexModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public IndexModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<EventAttendance> EventAttendance { get;set; }

        public async Task OnGetAsync()
        {
            EventAttendance = await _context.EventAttendances
                .Include(e => e.Event)
                .Include(e => e.Student).ToListAsync();
        }

        public class Lookup
        {
            //usage: priorityWeights[student.priorityRemaining || eventAttendance.priorityGiven]
            public double [] priorityWeights;
            //usage: gradeWeights[student.GradeLevel]
            public double [] gradeWeights;
            public Lookup(int eventCount, int gradeCount = 4)
            {
                priorityWeights = new double[eventCount];
                gradeWeights = new double[gradeCount];
                for(int i = 1; i <= eventCount; i++)
                {
                    priorityWeights[i] = (eventCount-(i-1))/(eventCount+1);
                }
                int j = 0;
                for(int i = 12; i >= 12 - gradeCount; i--)
                {
                    gradeWeights[i] = (gradeCount-j)/(gradeCount+1);
                    j++;
                }
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var attendances = await _context.EventAttendances.ToListAsync();
            if (attendances.Count() == 0)
            {
                return RedirectToPage("./Index");
            }
            var events = await _context.Events.ToListAsync();
            var students = await _context.Students.ToListAsync();

            Lookup LookupTable = new Lookup(events.Count());
            
            foreach (var evnt in events)
            {
                var attends = attendances.Where(a => a.EventID == evnt.EventID)
                                        .ToList();
                var studs = attends.Select(s => s.Student).ToList();

                foreach (var record in attends)
                {
                    var stud = studs.Where(s => s.Id == record.StudentID).SingleOrDefault();

                    var one = Math.Pow(LookupTable.priorityWeights[record.Priority], 2);
                    var two = Math.Pow(LookupTable.priorityWeights[stud.priorityRemaining], 2);
                    var three = Math.Pow(LookupTable.gradeWeights[stud.GradeLevel], 2);

                    var wght = Math.Sqrt(one + two + three);
                    record.Weight = wght;
                }

                attends.OrderBy(a => a.Weight);
                int signed = 0;

                foreach (var record in attends)
                {
                    if(signed < evnt.MaxAttendance)
                    {
                        record.Assigned = true;
                        signed++;
                    }
                    else
                    {
                        foreach (var stu in students.Where(s => s.Id == record.StudentID))
                        {
                            if (stu.priorityRemaining > 1)
                            {
                                stu.priorityRemaining--;
                            }
                            _context.Update(stu);
                        }
                        record.Assigned = false;
                        _context.Update(record);
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
