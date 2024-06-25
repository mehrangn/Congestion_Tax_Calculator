namespace Congestion_Tax_Calculator.Domain
{
    public class TaxedTimeInterval
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public int TaxAmout { get; }
        public string CityName { get; set; }
        public TaxedTimeInterval(TimeSpan start, TimeSpan end, int taxAmount)
        {
            Start = start;
            End = end;
            TaxAmout = taxAmount;
        }

        public bool IsWithinTimeInterval(TimeSpan time)
        {
            return time >= Start && time <= End;
        }
    }
}
