using Congestion_Tax_Calculator.Domain;
using Nager.Date;
using Nager.Date.Models;

namespace Congestion_Tax_Calculator.BusinessLogic
{
    public class CongestionTaxCalculation
    {
        public List<TollRecord> TaxCalculation(ICollection<VehiclePassing> vehiclePassings)
        {
            var tollRecords = new List<TollRecord>();

            var dailyPasses = vehiclePassings
                .GroupBy(vp => vp.RegisterTime.Date)
                .Where(group => !IsTaxFreeDay(group.Key))
                .ToDictionary(group => group.Key, group => group.ToList());

            foreach (var day in dailyPasses.Keys)
            {
                var dailyTaxRecords = CalculateDailyTaxRecords(dailyPasses[day]);
                tollRecords.AddRange(dailyTaxRecords);
            }

            return tollRecords;
        }


        private List<TollRecord> CalculateDailyTaxRecords(List<VehiclePassing> vehiclePassings)
        {
            var dailyTaxRecords = new List<TollRecord>();
            DateTime? lastTaxedTime = null;
            int maxTaxAmount = 0;

            foreach (var vehiclePassing in vehiclePassings.OrderBy(p => p.RegisterTime))
            {
                if (vehiclePassing.Vehicle.VehicleType != VehicleType.Car || vehiclePassing.Vehicle.VehicleType != VehicleType.Motorcycles)
                {
                    continue;
                }

                if (lastTaxedTime.HasValue && (vehiclePassing.RegisterTime - lastTaxedTime.Value).TotalMinutes <= 60)
                {
                    maxTaxAmount = Math.Max(maxTaxAmount, GetTaxAmount(vehiclePassing.RegisterTime));
                }
                else
                {
                    if (lastTaxedTime.HasValue)
                    {
                        dailyTaxRecords.Add(CreateTollRecord(vehiclePassing.Vehicle, lastTaxedTime.Value, maxTaxAmount));
                    }
                    maxTaxAmount = GetTaxAmount(vehiclePassing.RegisterTime);
                    lastTaxedTime = vehiclePassing.RegisterTime;
                }
            }

            if (lastTaxedTime.HasValue)
            {
                dailyTaxRecords.Add(CreateTollRecord(vehiclePassings.Last().Vehicle, lastTaxedTime.Value, maxTaxAmount));
            }

            return dailyTaxRecords;
        }

        private TollRecord CreateTollRecord(Vehicle vehicle, DateTime time, int taxAmount)
        {
            return new TollRecord
            {
                Id = Guid.NewGuid(),
                Vehicle = vehicle,
                RegisteredTime = time,
                TollFee = taxAmount
            };
        }

        private int GetTaxAmount(DateTime time)
        {
            List<TaxedTimeInterval> taxedTimeIntervals = GetTimeIntervalAndTaxAmout();
            var interval = taxedTimeIntervals.FirstOrDefault(i => i.IsWithinTimeInterval(time.TimeOfDay));
            return interval?.TaxAmout ?? 0;
        }

        //for better use and to be able to use this on other cities its better to add this to database
        //so each city have their own time intervals and tax rate so i just kept this short and hardcode it's date
        private List<TaxedTimeInterval> GetTimeIntervalAndTaxAmout()
        {
            List<TaxedTimeInterval> taxedTimeIntervals = new List<TaxedTimeInterval>
            {
                new TaxedTimeInterval(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59), 8),
                new TaxedTimeInterval(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59), 13),
                new TaxedTimeInterval(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59), 18),
                new TaxedTimeInterval(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59), 13),
                new TaxedTimeInterval(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59), 8),
                new TaxedTimeInterval(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 59), 13),
                new TaxedTimeInterval(new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59), 18),
                new TaxedTimeInterval(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59), 13),
                new TaxedTimeInterval(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 59), 8),
                new TaxedTimeInterval(new TimeSpan(18, 30, 0), new TimeSpan(5, 59, 59), 0)
            };

            return taxedTimeIntervals;
        }

        private bool IsTaxFreeDay(DateTime date)
        {
            List<DateTime> holidaysFreeDates = GetPublicHolidayFreeDays();
            return date.Month == 7 || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || holidaysFreeDates.Contains(date);
        }

        private List<DateTime> GetPublicHolidayFreeDays()
        {
            List<DateTime> holidayDates = new List<DateTime>();

            IEnumerable<Holiday> publicHolidays = HolidaySystem.GetHolidays(2013, "SE");

            foreach (Holiday holiday in publicHolidays)
            {
                holidayDates.Add(holiday.Date);
                holidayDates.Add(holiday.Date.AddDays(-1));
            }

            return holidayDates;
        }
    }
}
