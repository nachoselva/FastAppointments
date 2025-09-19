namespace Payments.Application.Implementations.Bill.Create
{
    using Common.Application;
    using Common.Application.CQRS;
    using Common.Application.Repositories;
    using Common.Models.Payments;
    using FluentResults;
    using Payments.Application.Abstractions;
    using Payments.Domain.Abstractions;
    using Payments.Domain.Commands;
    using Payments.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CreateBillCommandHandler(
        IBillRepository billRepository,
        IPaymentEntityRepository paymentEntityRepository,
        IUnitOfWork unitOfWork,
        IEventPublisher<CreateBillEventBody> billEventPublisher,
        IPaymentEntityService paymentEntityService) : ICommandHandler<CreateBillCommand, Guid>
    {
        public async Task<Result<Guid>> HandleAsync(CreateBillCommand command, CancellationToken cancellationToken)
        {
            var domainCommand = new CreateBillDomainCommand(
                new PaymentEntityDomainCommand(command.Sender.ClientId, command.Sender.ProviderId, command.Sender.CompanyId),
                new PaymentEntityDomainCommand(command.Receiver.ClientId, command.Receiver.ProviderId, command.Receiver.CompanyId),
                command.BillItems.Select(bi => 
                    new CreateBillItemDomainCommand(
                        bi.Description,
                        bi.PricePerUnit,
                        bi.UnitsCount,
                        bi.UnitsName,
                        bi.BillItemSources.Select(src => new CreateBillItemSourceDomainCommand(src.SourceType, src.SourceId)))),
                command.BillSources.Select(src => new CreateBillSourceDomainCommand(src.SourceType, src.SourceId)));

            var billResult = await Bill.CreateAsync(paymentEntityService, domainCommand);

            if (billResult.IsFailed)
                return Result.Fail(billResult.Errors);

            var bill = billResult.Value;    

            if (bill.Sender.PaymentEntity.Id == default)
                await paymentEntityRepository.AddAsync(bill.Sender.PaymentEntity);

            if (bill.Receiver.PaymentEntity.Id == default)
                await paymentEntityRepository.AddAsync(bill.Receiver.PaymentEntity);

            await billRepository.AddAsync(bill);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await billEventPublisher.PublishAsync(new CreateBillEventBody(bill.Id), cancellationToken);

            return bill.Id;
        }
    }
}
