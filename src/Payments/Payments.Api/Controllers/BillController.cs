using Common.Application.CQRS;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Implementations.Bill.Create;
using Payments.Application.Implementations.PaymentEntity.Common;

namespace Payments.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BillController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public BillController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    // Request -> Command

    [HttpPost]
    public async Task<Result<Guid>> CreateBill([FromBody] CreateBillRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBillCommand(
            new PaymentEntityCommand(request.Sender.ClientId, request.Sender.ProviderId, request.Sender.CompanyId),
            new PaymentEntityCommand(request.Receiver.ClientId, request.Receiver.ProviderId, request.Receiver.CompanyId),
            request.BillItems.Select(bi =>
                new CreateBillItemCommand(
                    bi.Description,
                    bi.PricePerUnit,
                    bi.UnitsCount,
                    bi.UnitsName,
                    bi.BillItemSources.Select(src => new CreateBillItemSourceCommand(src.SourceType, src.SourceId))
                    )
            ),
            request.BillSources.Select(src => new CreateBillSourceCommand(src.SourceType, src.SourceId))
        );

        return await _commandDispatcher.DispatchAsync<CreateBillCommand, Guid>(command, cancellationToken);
    }
}
