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
                },
                new Student
                {
                    LastName = "Dane",
                    FirstMidName = "Jane",
                    Gender = Gender.F,
                    GradeLevel = 9
                },
                new Student
                {
                    LastName = "Porter",
                    FirstMidName = "David",
                    Gender = Gender.M,
                    GradeLevel = 11
                },
                new Student
                {
                    LastName = "Raines",
                    FirstMidName = "Avery",
                    Gender = Gender.M,
                    GradeLevel = 10
                },
                new Student
                {
                    LastName = "Pierce",
                    FirstMidName = "Mason",
                    Gender = Gender.M,
                    GradeLevel = 9
                },
                new Student
                {
                    LastName = "Moe",
                    FirstMidName = "Edna",
                    Gender = Gender.F,
                    GradeLevel = 12
                },
                new Student
                {
                    LastName = "Indigo",
                    FirstMidName = "Violet",
                    Gender = Gender.F,
                    GradeLevel = 11
                },
                new Student
                {
                    LastName = "Baby",
                    FirstMidName = "DaJoker",
                    Gender = Gender.F,
                    GradeLevel = 10
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
                    LastName = "Wells",
                    FirstMid = "Sara",
                    Admin = true
                },
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
                    MaxAttendance = 2,
                    StartTime = Convert.ToDateTime("2020-09-15 13:00:00")
                },
                new Event
                {
                    Name = "Paintball 3pm",
                    MaxAttendance = 2,
                    StartTime = Convert.ToDateTime("2020-09-15 15:00:00")
                },
                new Event
                {
                    Name = "High Ropes 2pm",
                    MaxAttendance = 2,
                    StartTime = Convert.ToDateTime("2020-09-15 14:00:00")
                },
                new Event
                {
                    Name = "Kayacking 4pm",
                    MaxAttendance = 2,
                    StartTime = Convert.ToDateTime("2020-09-15 16:00:00")
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