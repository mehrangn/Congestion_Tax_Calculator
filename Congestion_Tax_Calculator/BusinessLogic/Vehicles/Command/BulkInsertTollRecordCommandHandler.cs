using AutoMapper;
using Congestion_Tax_Calculator.BusinessLogic.Persistance;
using Congestion_Tax_Calculator.BusinessLogic.Vehicles.Query;
using Congestion_Tax_Calculator.Domain;
using MediatR;
using System.Collections.Generic;

namespace Congestion_Tax_Calculator.BusinessLogic.Vehicles.Command
{
    public class BulkInsertTollRecordCommandHandler : IRequestHandler<BulkInsertTollRecordCommand, ICollection<TollRecord>>
    {
        private readonly ITollRecordRepository _tollRecordRepository;
        private readonly CongestionTaxCalculation _congestionTaxCalculation;

        public BulkInsertTollRecordCommandHandler(ITollRecordRepository tollRecord)
        {
            _tollRecordRepository = tollRecord;
        }

        public async Task<ICollection<TollRecord>> Handle(BulkInsertTollRecordCommand request, CancellationToken cancellationToken)
        {
            List<TollRecord> tollRecords = new List<TollRecord>();

            List<TollRecord> Records = _congestionTaxCalculation.TaxCalculation(request.VehiclePassing);

            await _tollRecordRepository.BulkInsert(Records);

            return tollRecords;
        }
    }
}
