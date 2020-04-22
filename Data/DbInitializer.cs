using TRAILES.Data;
using TRAILES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRAILES.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            if (context.Students.Any())
            {
                return;
            }

            var user = new IdentityUser("admin@bchstest.com");
            user.EmailConfirmed = true;
            await userManager.CreateAsync(user, "$Password1");

            await SeedDb(context, userManager);

        }

        public static async Task SeedDb(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            var students = new Student[]{
                new Student
                {
                    Fname = "Carson",
                    Lname = "Alexander",
                    Gender = Gender.Male,
                    GradeLevel = 9
                },
                new Student
                {
                    Fname = "Meredith",
                    Lname = "Alonso",
                    Gender = Gender.Female,
                    GradeLevel = 9
                },
                new Student
                {
                    Fname = "Arturo",
                    Lname = "Anand",
                    Gender = Gender.Male,
                    GradeLevel = 10
                },
                new Student
                {
                    Fname = "Gytis",
                    Lname = "Barzdukas",
                    Gender = Gender.Female,
                    GradeLevel = 10
                },
                new Student
                {
                    Fname = "Yan",
                    Lname = "Li",
                    Gender = Gender.Male,
                    GradeLevel = 11
                },
                new Student
                {
                    Fname = "Peggy",
                    Lname = "Justice",
                    Gender = Gender.Female,
                    GradeLevel = 11
                },
                new Student
                {
                    Fname = "Laura",
                    Lname = "Norman",
                    Gender = Gender.Female,
                    GradeLevel = 12
                },
                new Student
                {
                    Fname = "Nino",
                    Lname = "Olivetto",
                    Gender = Gender.Male,
                    GradeLevel = 12
                }
            };
            foreach (var stu  in students)
            {
                stu.EmailConfirmed = true;
                stu.PhoneNumberConfirmed = true;
                stu.TwoFactorEnabled = false;
                stu.LockoutEnabled = true;
                stu.AccessFailedCount = 0;
                var email = stu.Fname.First() + stu.Lname + "@bchstest.com";
                stu.UserName = email;
                await userManager.CreateAsync(stu, "Password1$");
            }
            await context.SaveChangesAsync();

            await context.Cabins.AddRangeAsync(
                new Cabin{Name = "Everest #101",Gender = Gender.Male},
                new Cabin{Name = "Everest #102",Gender = Gender.Male},
                new Cabin{Name = "Everest #103",Gender = Gender.Male},
                new Cabin{Name = "Everest #104",Gender = Gender.Male},
                new Cabin{Name = "Everest #201",Gender = Gender.Male},
                new Cabin{Name = "Everest #202",Gender = Gender.Male},
                new Cabin{Name = "Everest #203",Gender = Gender.Male},
                new Cabin{Name = "Everest #204",Gender = Gender.Male},
                new Cabin{Name = "Alpine #101",Gender = Gender.Male},
                new Cabin{Name = "Alpine #102",Gender = Gender.Male},
                new Cabin{Name = "Alpine #103",Gender = Gender.Male},
                new Cabin{Name = "Alpine #104",Gender = Gender.Male},
                new Cabin{Name = "Alpine #201",Gender = Gender.Male},
                new Cabin{Name = "Alpine #202",Gender = Gender.Male},
                new Cabin{Name = "Alpine #203",Gender = Gender.Male},
                new Cabin{Name = "Alpine #204",Gender = Gender.Male},
                new Cabin{Name = "Dogwood #1",Gender = Gender.Male},
                new Cabin{Name = "Dogwood #2",Gender = Gender.Male},
                new Cabin{Name = "Chaparral #1",Gender = Gender.Male},
                new Cabin{Name = "Chaparral #2",Gender = Gender.Male},
                new Cabin{Name = "Holly #1",Gender = Gender.Male},
                new Cabin{Name = "Holly #2",Gender = Gender.Male},
                new Cabin{Name = "McKinley #1",Gender = Gender.Male},
                new Cabin{Name = "McKinley #2",Gender = Gender.Male},
                new Cabin{Name = "McKinley #3",Gender = Gender.Male},
                new Cabin{Name = "McKinley #4",Gender = Gender.Male},
                new Cabin{Name = "Baker #1",Gender = Gender.Male},
                new Cabin{Name = "Alder #101",Gender = Gender.Female},
                new Cabin{Name = "Alder #102",Gender = Gender.Female},
                new Cabin{Name = "Alder #103",Gender = Gender.Female},
                new Cabin{Name = "Alder #104",Gender = Gender.Female},
                new Cabin{Name = "Alder #201",Gender = Gender.Female},
                new Cabin{Name = "Alder #202",Gender = Gender.Female},
                new Cabin{Name = "Alder #203",Gender = Gender.Female},
                new Cabin{Name = "Alder #204",Gender = Gender.Female},
                new Cabin{Name = "Laurel #101",Gender = Gender.Female},
                new Cabin{Name = "Laurel #102",Gender = Gender.Female},
                new Cabin{Name = "Laurel #103",Gender = Gender.Female},
                new Cabin{Name = "Laurel #104",Gender = Gender.Female},
                new Cabin{Name = "Laurel #201",Gender = Gender.Female},
                new Cabin{Name = "Laurel #202",Gender = Gender.Female},
                new Cabin{Name = "Laurel #203",Gender = Gender.Female},
                new Cabin{Name = "Laurel #204",Gender = Gender.Female}, 
                new Cabin{Name = "Willow #101",Gender = Gender.Female},
                new Cabin{Name = "Willow #102",Gender = Gender.Female},
                new Cabin{Name = "Willow #103",Gender = Gender.Female},
                new Cabin{Name = "Willow #104",Gender = Gender.Female},
                new Cabin{Name = "Willow #201",Gender = Gender.Female},
                new Cabin{Name = "Willow #202",Gender = Gender.Female},
                new Cabin{Name = "Willow #203",Gender = Gender.Female},
                new Cabin{Name = "Willow #204",Gender = Gender.Female}, 
                new Cabin{Name = "Pinyon #101",Gender = Gender.Female},
                new Cabin{Name = "Pinyon #102",Gender = Gender.Female},
                new Cabin{Name = "Pinyon #103",Gender = Gender.Female},
                new Cabin{Name = "Pinyon #104",Gender = Gender.Female},
                new Cabin{Name = "Pinyon #201",Gender = Gender.Female},
                new Cabin{Name = "Pinyon #202",Gender = Gender.Female},
                new Cabin{Name = "Pinyon #203",Gender = Gender.Female},
                new Cabin{Name = "Pinyon #204",Gender = Gender.Female} 
            );
            await context.SaveChangesAsync();

            await context.Events.AddRangeAsync(
                new Event{Name="Paintball 1pm",MaxAttendance = 2, StartTime = Convert.ToDateTime("2020-09-15 13:00:00")},
                new Event{Name="Paintball 3pm",MaxAttendance = 2, StartTime = Convert.ToDateTime("2020-09-15 15:00:00")},
                new Event{Name="High Ropes 2pm",MaxAttendance = 2, StartTime = Convert.ToDateTime("2020-09-15 14:00:00")},
                new Event{Name="Kayacking 4pm",MaxAttendance = 2, StartTime = Convert.ToDateTime("2020-09-15 16:00:00")}
            );
            await context.SaveChangesAsync();
        }
    }
}