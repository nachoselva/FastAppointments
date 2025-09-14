namespace Payments.Application.Bill.Create
{
    using Common.Application.CQRS;
    using Common.Application.Repositories;
    using FluentResults;
    using Payments.Application.Abstractions;
    using Payments.Application.Events;
    using Payments.Application.Implementations.Bill.Create;
    using Payments.Domain.Abstractions;
    using Payments.Domain.Commands;
    using Payments.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CreateBillCommandHandler : ICommandHandler<CreateBillCommand, Guid>
    {
        private readonly IBillRepository _billRepository;
        private readonly IPaymentEntityRepository _paymentEntityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher<CreateBillEventBody> _billEventPublisher;
        private readonly IPaymentEntityService _paymentEntityService;

        public CreateBillCommandHandler(
            IBillRepository billRepository,
            IPaymentEntityRepository paymentEntityRepository,
            IUnitOfWork unitOfWork,
            IEventPublisher<CreateBillEventBody> billEventPublisher,
            IPaymentEntityService paymentEntityService)
        {
            _billRepository = billRepository;
            _paymentEntityRepository = paymentEntityRepository;
            _unitOfWork = unitOfWork;
            _billEventPublisher = billEventPublisher;
            _paymentEntityService = paymentEntityService;
        }

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

            var billResult = await Bill.CreateAsync(_paymentEntityService, domainCommand);

            if (billResult.IsFailed)
                return Result.Fail(billResult.Errors);

            var bill = billResult.Value;    

            if (bill.Sender.PaymentEntity.Id == default)
                await _paymentEntityRepository.AddAsync(bill.Sender.PaymentEntity);

            if (bill.Receiver.PaymentEntity.Id == default)
                await _paymentEntityRepository.AddAsync(bill.Receiver.PaymentEntity);

            await _billRepository.AddBill(bill);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _billEventPublisher.PublishAsync(new CreateBillEventBody(bill.Id), cancellationToken);

            return bill.Id;
        }
    }
}
