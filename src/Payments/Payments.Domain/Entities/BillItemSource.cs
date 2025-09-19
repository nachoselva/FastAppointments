namespace Payments.Domain.Entities
{
    using Common.Domain;
    using Payments.Domain.Commands;

    public class BillItemSource : DomainEntity
    {
        public string SourceType { get; private set; } = string.Empty;
        public Guid SourceId { get; private set; }

        private BillItemSource(string sourceType, Guid sourceId)
        {
            SourceType = sourceType;
            SourceId = sourceId;
        }
        
        protected BillItemSource()
        {

        }

        public Guid BillItemId { get; private set; }
        public virtual BillItem BillItem { get; private set; } = null!;

        internal static BillItemSource Create(CreateBillItemSourceDomainCommand command)
        {
            return new BillItemSource(command.SourceType, command.SourceId);
        }
    }
}
