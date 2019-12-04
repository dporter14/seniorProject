using TRAILES.Models;
using System;
using System.Linq;

namespace TRAILES.Data
{
    public static class DbInit
    {
        public static void Init(TRAILESContext context)
        {
            context.Database.EnsureCreated();

            if (context.Student.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student
                {
                    LastName = "Doe",
                    FirstMidName = "John",
                    Gender = Gender.M,
                    GradeLevel = 12
                }
            };
            foreach (Student s in students)
            {
                context.Student.Add(s);
            }
            context.SaveChanges();

            var employees = new FacStaff[]
            {
                new FacStaff
                {
                    LastName = "Deer",
                    FirstMid = "Jane"
                }
            };
            foreach (FacStaff f in employees)
            {
                context.FacStaff.Add(f);
            }
            context.SaveChanges();

            var events = new Event[]
            {
                new Event
                {
                    Name = "Paintball 1pm",
                    MaxAttendance = 20,
                    StartTime = Convert.ToDateTime("2020-09-15 13:00:00")
                },
                new Event
                {
                    Name = "Paintball 3pm",
                    MaxAttendance = 20,
                    StartTime = Convert.ToDateTime("2020-09-15 15:00:00")
                },
                new Event
                {
                    Name = "High Ropes 1pm",
                    MaxAttendance = 20,
                    StartTime = Convert.ToDateTime("2020-09-15 13:00:00")
                },
                new Event
                {
                    Name = "High Ropes 3pm",
                    MaxAttendance = 20,
                    StartTime = Convert.ToDateTime("2020-09-15 15:00:00")
                }
            };
            foreach (Event e in events)
            {
                context.Event.Add(e);
            }
            context.SaveChanges();

            var cabins = new Cabin[]
            {
                new Cabin
                {
                    Name="Everest #101",
                    Gender=Gender.M,
                    BedCount=10
                },
                new Cabin
                {
                    Name="Alder #202",
                    Gender = Gender.F,
                    BedCount=10
                }
            };
            foreach (Cabin c in cabins)
            {
                context.Cabin.Add(c);
            }
            context.SaveChanges();
        }
    }
}