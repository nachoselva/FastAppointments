namespace Appointments.Application.Abstractions
{
    using Appointments.Domain.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IEventRepository
    {
        Task AddAsync(Event @event);
        Task<Event?> GetAsync(Guid eventId);
        Task<IEnumerable<Event>> GetAsync(DateTime? scheduledFrom, DateTime? scheduledTo);
    }
}
