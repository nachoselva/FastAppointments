namespace Payments.Api.Controllers
{
    using Common.Application.CQRS;
    using Common.Models.Setups;
    using FluentResults;
    using Microsoft.AspNetCore.Mvc;
    using Payments.Application.Implementations.Bill.Create;
    using Payments.Application.Implementations.PaymentEntity.Common;

    [ApiController]
    [Route("[controller]")]
    public class BillsController(ICommandDispatcher commandDispatcher) : ControllerBase
    {
        [HttpPost]
        [Produces<Guid>]
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

            return await commandDispatcher.DispatchAsync<CreateBillCommand, Guid>(command, cancellationToken);
        }
    }
}