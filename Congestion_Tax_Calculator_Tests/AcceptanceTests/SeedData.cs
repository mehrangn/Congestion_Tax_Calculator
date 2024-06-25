using Congestion_Tax_Calculator.DataAccess.Persistance;
using Congestion_Tax_Calculator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Congestion_Tax_Calculator_Tests.AcceptanceTests
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaxCalculatorDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaxCalculatorDbContext>>()))
            {
                if (context.Database.EnsureCreated())
                {
                    context.TollRecord.AddRange(
                        new TollRecord { Id = Guid.NewGuid(), RegisteredTime = DateTime.Now, TollFee = 56, Vehicle = new Car { PlateNumber = "aws123" } },

                            new TollRecord
                            {
                                Id = Guid.NewGuid(),
                                RegisteredTime = DateTime.Now,
                                TollFee = 23,
                                Vehicle = new MilitaryVehicle { PlateNumber = "ads123" }
                            }
                     );

                    context.SaveChanges();
                }
            }
        }
    }
}
