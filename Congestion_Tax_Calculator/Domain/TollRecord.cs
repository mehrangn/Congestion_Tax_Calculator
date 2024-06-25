namespace Congestion_Tax_Calculator.Domain
{
    //i added vehicle here so i dont make 2 tables and do everything i need with one table in sql and don't make project too complex
    public class TollRecord
    {
        public Guid Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime RegisteredTime { get; set; }
        public int TollFee { get; set; }
    }
}
