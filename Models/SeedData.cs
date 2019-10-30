using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TRAILES.Data;
using System;
using System.Linq;

namespace TRAILES.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TRAILESContext(
                serviceProvider.GetRequiredService<DbContextOptions<TRAILESContext>>()))
            {
                //Look for any Cabins
                if (context.Cabin.Any())
                {
                    return; // DB is Seeded
                }

                context.Cabin.AddRange(
                    new Cabin
                    {
                        Name = "Everest #101",
                        AddDate = DateTime.Now,
                        Gender = "Male",
                        BedCount = 9,
                        BedsFilled = 0
                    },
                    new Cabin
                    {
                        Name = "Everest #203",
                        AddDate = DateTime.Now,
                        Gender = "Male",
                        BedCount = 9,
                        BedsFilled = 0
                    },
                    new Cabin
                    {
                        Name = "Alpine #101",
                        AddDate = DateTime.Now,
                        Gender = "Male",
                        BedCount = 9,
                        BedsFilled = 0
                    },
                    new Cabin
                    {
                        Name = "Holly #2",
                        AddDate = DateTime.Now,
                        Gender = "Male",
                        BedCount = 9,
                        BedsFilled = 0
                    },
                    new Cabin
                    {
                        Name = "Alder #104",
                        AddDate = DateTime.Now,
                        Gender = "Female",
                        BedCount = 9,
                        BedsFilled = 0
                    },
                    new Cabin
                    {
                        Name = "Alder #202",
                        AddDate = DateTime.Now,
                        Gender = "Female",
                        BedCount = 9,
                        BedsFilled = 0
                    },
                    new Cabin
                    {
                        Name = "Laurel #202",
                        AddDate = DateTime.Now,
                        Gender = "Female",
                        BedCount = 9,
                        BedsFilled = 0
                    },
                    new Cabin
                    {
                        Name = "Pinyon #101",
                        AddDate = DateTime.Now,
                        Gender = "Female",
                        BedCount = 9,
                        BedsFilled = 0
                    }
                );
                context.SaveChanges();
            }
        }
    }
}