namespace Payments.Domain.Entities
{
    using Common.Domain;
    using System.Collections.Generic;

    public class PaymentAccount : DomainEntity
    {
        private PaymentAccount(string accountType, string accountData)
        {
            AccountType = accountType;
            AccountData = accountData;
        }

        protected PaymentAccount()
        {

        }

        public string AccountType { get; private set; }
        public string AccountData { get; private set; }

        public Guid PaymentEntityId { get; private set; }
        public virtual PaymentEntity PaymentEntity { get; private set; } = null!;
        public virtual ICollection<Bill> SentBills { get; private set; } = null!;
        public virtual ICollection<Bill> ReceivedBills { get; private set; } = null!;

        internal static PaymentAccount Create(string accountType, string accountData, PaymentEntity paymentEntity)
        {
            return new PaymentAccount(accountType, accountData)
            {
                PaymentEntity = paymentEntity
            };
        }
    }
}
