namespace Payments.Application.Implementations.Bill.Create
{
    using Payments.Application.Implementations.PaymentEntity.Common;

    public sealed record CreateBillRequest(
        PaymentEntityRequest Sender,
        PaymentEntityRequest Receiver,
        IEnumerable<CreateBillItemRequest> BillItems,
        IEnumerable<CreateBillSourceRequest> BillSources);
}
