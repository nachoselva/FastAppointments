namespace Payments.Domain.Entities
{
    using Common.Domain;
    using FluentResults;
    using Payments.Domain.Commands;

    public class BillSource : DomainEntity
    {
        private BillSource(string sourceType, Guid sourceId)
        {
            SourceType = sourceType;
            SourceId = sourceId;
        }

        protected BillSource()
        {

        }

        public string SourceType { get; private set; } = string.Empty;
        public Guid SourceId { get; private set; }

        public Guid BillId { get; private set; }
        public virtual Bill Bill { get; private set; } = null!;

        internal static Result<BillSource> Create(CreateBillSourceDomainCommand command)
        {
            return new BillSource(command.SourceType, command.SourceId);
        }
    }
}
