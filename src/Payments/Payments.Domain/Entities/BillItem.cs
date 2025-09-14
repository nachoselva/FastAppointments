namespace Payments.Domain.Entities
{
    using Common.Domain;
    using FluentResults;
    using Payments.Domain.Commands;
    using System.Collections.Generic;

    public class BillItem : DomainEntity
    {
        public string Description { get; private set; }
        public decimal PricePerUnit { get; private set; }
        public int UnitsCount { get; private set; }
        public string UnitsName { get; private set; }

        private BillItem(string description, decimal pricePerUnit, int unitsCount, string unitsName)
        {
            Description = description;
            PricePerUnit = pricePerUnit;
            UnitsCount = unitsCount;
            UnitsName = unitsName;
        }
        protected BillItem()
        {

        }

        public Guid BillId { get; private set; }
        public virtual Bill Bill { get; private set; } = null!;
        public virtual ICollection<BillItemSource> BillItemSources { get; private set; } = null!;

        internal static Result<BillItem> Create(CreateBillItemDomainCommand command)
        {
            if (command.UnitsCount < 0)
                return Result.Fail("Units can't be negative");

            return new BillItem(command.Description, command.PricePerUnit, command.UnitsCount, command.UnitsName)
            {
                BillItemSources = command.BillItemSources.Select(BillItemSource.Create).ToList()
            };
        }
    }
}
