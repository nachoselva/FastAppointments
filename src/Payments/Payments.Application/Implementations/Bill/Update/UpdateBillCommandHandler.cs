namespace Payments.Application.Implementations.Bill.Update
{
    using Common.Application.CQRS;
    using Common.Application.Repositories;
    using FluentResults;
    using Payments.Application.Abstractions;
    using Payments.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UpdateBillCommandHandler(
        IBillRepository billRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<UpdateBillCommand, Guid>
    {
        public async Task<Result<Guid>> HandleAsync(UpdateBillCommand command, CancellationToken cancellationToken)
        {
            Bill? bill = await billRepository.GetByIdAsync(command.Id);

            if (bill == null)
                return Result.Fail("Bill does not exists");

            bill.UpdateStatus(command.Status);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return bill.Id;
        }
    }
}
