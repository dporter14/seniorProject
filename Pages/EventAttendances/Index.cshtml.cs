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
                .Include(e => e.Student)
                .OrderByDescending(a => a.Assigned)
                .ThenBy(a => a.Event.StartTime)
                .ToListAsync();
        }

        public class Lookup
        {
            //usage: priorityWeights[student.priorityRemaining || eventAttendance.priorityGiven]
            public double [] priorityWeights;
            //usage: gradeWeights[student.GradeLevel]
            public double [] gradeWeights;
            public Lookup(int eventCount, int gradeCount = 4)
            {
                priorityWeights = new double[eventCount+1];
                gradeWeights = new double[13];
                for(int i = 1; i <= eventCount; i++)
                {
                    priorityWeights[i] = ((eventCount-(i-1d))/(eventCount+1d));
                }
                int j = 0;
                for(int i = 12; i > 12 - gradeCount; i--)
                {
                    gradeWeights[i] = ((gradeCount-j)/(gradeCount+1d)); 
                    j++;
                }
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var attendances = _context.EventAttendances.Select(a => a);
            if (attendances.Count() == 0)
            {
                return RedirectToPage("./Index");
            }
            var events = _context.Events.Select(e => e);
            var students = _context.Students.Select(s => s);

            Lookup LookupTable = new Lookup(events.Count());
            
            foreach (var evnt in events)
            {
                var attends = attendances.Where(a => a.EventID == evnt.EventID)
                                        .Select(a => a);
                var studs = attends.Select(s => s.Student);

                foreach (var record in attends)
                {
                    var stud = studs.Where(s => s.Id == record.StudentID).SingleOrDefault();

                    var one = Math.Pow(LookupTable.priorityWeights[record.Priority], 2);
                    var two = Math.Pow(LookupTable.priorityWeights[stud.priorityRemaining], 2);
                    var three = Math.Pow(LookupTable.gradeWeights[stud.GradeLevel], 2);

                    var wght = Math.Sqrt(one + two + three);
                    record.Weight = wght * 1000d;
                }

                var sortAtt = await attends.ToListAsync();
                int n = sortAtt.Count();

                for(int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (sortAtt[j].Weight < sortAtt[j + 1].Weight)
                        {
                            EventAttendance temp = sortAtt[j];
                            sortAtt[j] = sortAtt[j+1];
                            sortAtt[j+1] = temp;
                        }
                    }
                }

                int signed = 0;
                foreach (var record in sortAtt)
                {
                    if(signed < evnt.MaxAttendance)
                    {
                        record.Assigned = true;
                        signed++;
                    }
                    else
                    {
                        var stu = record.Student;
                        if (stu.priorityRemaining > 1)
                        {
                            stu.priorityRemaining--;
                        }
                        //_context.Update(stu);
                        record.Assigned = false;
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
