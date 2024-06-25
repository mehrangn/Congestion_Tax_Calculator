using AutoMapper;
using Congestion_Tax_Calculator.BusinessLogic.Persistance;
using Congestion_Tax_Calculator.Domain;
using MediatR;

namespace Congestion_Tax_Calculator.BusinessLogic.Vehicles.Query
{
    public class GetAllTollRecordsQueryHandler : IRequestHandler<GetAllTollRecordsQuery, ICollection<TollRecord>>
    {
        private readonly ITollRecordRepository _vehicleRepository;
        public GetAllTollRecordsQueryHandler(ITollRecordRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ICollection<TollRecord>> Handle(GetAllTollRecordsQuery request, CancellationToken cancellationToken)
        {
            var tollRecord = await _vehicleRepository.GetAllAsync();
            return tollRecord;
        }
    }
}