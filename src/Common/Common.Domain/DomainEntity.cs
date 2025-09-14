namespace Common.Domain
{
    public abstract class DomainEntity
    {
        protected DomainEntity()
        {

        }

        public Guid Id { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public DateTime? ModifiedOn { get; private set; }
    }
}
