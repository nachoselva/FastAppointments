namespace Setups.Domain.Entities
{
    using Common.Domain;
    using Setups.Domain.Commands;
    using System;

    public class Location : DomainEntity
    {
        private Location(string name, string? floor, string? room)
        {
            Name = name;
            Floor = floor;
            Room = room;
        }

        protected Location() { }

        public string Name { get; private set; } = string.Empty;
        public string? Floor { get; private set; }
        public string? Room { get; private set; }

        public Guid BuildingId { get; private set; } = default;
        public virtual Building Building { get; private set; } = null!;

        internal static Location Create(CreateLocationDomainCommand model)
        {
            return new Location(model.Name, model.Floor, model.Room)
            {
                Building = null!
            };
        }
    }
}
