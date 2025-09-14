namespace Payments.Application.Implementations.Bill.Update
{
    using Common.Application.CQRS;
    using Payments.Domain.Enums;

    public sealed record UpdateBillCommand(Guid Id, BillStatus Status) : ICommand<Guid>;
}
