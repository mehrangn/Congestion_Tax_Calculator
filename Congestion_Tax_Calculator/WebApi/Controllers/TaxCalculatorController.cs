using AutoMapper;
using Congestion_Tax_Calculator.BusinessLogic.Vehicles.Command;
using Congestion_Tax_Calculator.BusinessLogic.Vehicles.Query;
using Congestion_Tax_Calculator.Domain;
using Congestion_Tax_Calculator.WebApi.Resource;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Congestion_Tax_Calculator.WebApi.Controllers;

[ApiController]
[Route("TaxCalculator")]
public class TaxCalculatorController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TaxCalculatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ICollection<TollRecordDTO>> GetTollRecordsAsync()
    {
        ICollection<TollRecordDTO> tollRecordsDTO = 
            _mapper.Map<List<TollRecordDTO>>(await _mediator.Send(new GetAllTollRecordsQuery()));
        return tollRecordsDTO;
    }

    [HttpPost]
    public async Task<ICollection<TollRecordDTO>> TollRecordsAsync([FromBody] ICollection<VehiclePassingDTO> vehiclePassings)
    {
        ICollection<VehiclePassing> vehiclePassingsEntities = _mapper.Map<ICollection<VehiclePassing>>(vehiclePassings);

        ICollection<TollRecordDTO> tollRecordsDTO =
            _mapper.Map<ICollection<TollRecordDTO>>(await _mediator.Send(new BulkInsertTollRecordCommand(vehiclePassingsEntities)));
        return tollRecordsDTO;
    }
}
