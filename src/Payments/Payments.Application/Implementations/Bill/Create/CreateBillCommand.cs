namespace Payments.Application.Implementations.Bill.Create
{
    using Common.Application.CQRS;
    using Payments.Application.Implementations.PaymentEntity.Common;

    public sealed record CreateBillCommand(
        PaymentEntityCommand Sender,
        PaymentEntityCommand Receiver,
        IEnumerable<CreateBillItemCommand> BillItems,
        IEnumerable<CreateBillSourceCommand> BillSources) : ICommand<Guid>
    {
    }
}
