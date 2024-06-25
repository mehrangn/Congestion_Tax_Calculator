using Congestion_Tax_Calculator.Domain;
using MediatR;

namespace Congestion_Tax_Calculator.BusinessLogic.Vehicles.Command
{
    public class BulkInsertTollRecordCommand : IRequest<ICollection<TollRecord>>
    {
        public ICollection<VehiclePassing> VehiclePassing { get; set; }

        public BulkInsertTollRecordCommand(ICollection<VehiclePassing> vehiclePassing)
        {
            VehiclePassing = vehiclePassing;
        }
    }
}
