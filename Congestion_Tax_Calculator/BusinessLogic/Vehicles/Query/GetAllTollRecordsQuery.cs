using Congestion_Tax_Calculator.Domain;
using MediatR;

namespace Congestion_Tax_Calculator.BusinessLogic.Vehicles.Query
{
    public class GetAllTollRecordsQuery : IRequest<ICollection<TollRecord>>
    {

    }
}
