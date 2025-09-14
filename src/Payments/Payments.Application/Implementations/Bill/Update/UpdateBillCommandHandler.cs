namespace Payments.Application.Implementations.Bill.Update
{
    using Common.Application.CQRS;
    using Common.Application.Repositories;
    using FluentResults;
    using Payments.Application.Abstractions;
    using Payments.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UpdateBillCommandHandler : ICommandHandler<UpdateBillCommand, Guid>
    {
        private readonly IBillRepository _billRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBillCommandHandler(
            IBillRepository billRepository,
            IUnitOfWork unitOfWork)
        {
            _billRepository = billRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> HandleAsync(UpdateBillCommand command, CancellationToken cancellationToken)
        {
            Bill? bill = await _billRepository.GetById(command.Id);

            if (bill == null)
                return Result.Fail("Bill does not exists");

            bill.UpdateStatus(command.Status);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return bill.Id;
        }
    }
}
