namespace Payments.Domain.Commands
{


    public sealed record CreateBillDomainCommand(PaymentEntityDomainCommand Sender, PaymentEntityDomainCommand Receiver, IEnumerable<CreateBillItemDomainCommand> BillItems, IEnumerable<CreateBillSourceDomainCommand> BillSources)
    {
    }
}
