
namespace Congestion_Tax_Calculator.WebApi.Resource
{
    public class TollRecordDTO
    {
        public Guid Id { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public DateTime RegisteredTime { get; set; }
        public int TollFee { get; set; }
    }
}
