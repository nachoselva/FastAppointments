namespace Payments.Domain.Entities
{
    using Common.Domain;
    using FluentResults;
    using Payments.Domain.Abstractions;
    using Payments.Domain.Commands;
    using Payments.Domain.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.Design;

    public class Bill : DomainEntity
    {
        public BillStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }

        private Bill(BillStatus status)
        {
            Status = status;
            BillItems = [];
            BillSources = [];
        }

        protected Bill()
        {

        }

        public Guid SenderId { get; private set; }
        public Guid ReceiverId { get; private set; }
        public virtual PaymentAccount Sender { get; private set; } = null!;
        public virtual PaymentAccount Receiver { get; private set; } = null!;
        public virtual ICollection<BillItem> BillItems { get; private set; } = null!;
        public virtual ICollection<BillSource> BillSources { get; private set; } = null!;

        public static async Task<Result<Bill>> CreateAsync(IPaymentEntityService paymentEntityService, CreateBillDomainCommand command)
        {
            Result<PaymentEntity> senderResult = await PaymentEntity.CreateOrUpdateAsync(paymentEntityService, command.Sender);
            Result<PaymentEntity> receiverResult = await PaymentEntity.CreateOrUpdateAsync(paymentEntityService, command.Receiver);
            Result<IEnumerable<BillItem>> billItemsResults = command.BillItems.BulkResult(BillItem.Create);
            Result<IEnumerable<BillSource>> billSourcesResults = command.BillSources.BulkResult(BillSource.Create);

            var result = Result.Merge(senderResult, receiverResult, billItemsResults, billSourcesResults);

            if (result.IsFailed)
                return result;

            return new Bill(BillStatus.Pending)
            {
                Sender = senderResult.Value.PaymentAccounts.First(),
                Receiver = receiverResult.Value.PaymentAccounts.First(),
                BillItems = billItemsResults.Value.ToList(),
                BillSources = billSourcesResults.Value.ToList(),
                TotalAmount = command.BillItems.Sum(bi => bi.UnitsCount * bi.PricePerUnit)
            };
        }

        public void UpdateStatus(BillStatus status)
        {
            Status = status;
        }
    }
}
