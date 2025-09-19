namespace Appointments.Infrastructure.Implementations
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Entities;
    using Appointments.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    internal class EventRepository(AppointmentsContext appointmentsContext) : IEventRepository
    {
        public async Task AddAsync(Event Event)
        {
            await appointmentsContext.Events.AddAsync(Event);
        }

        public async Task<Event?> GetAsync(Guid eventId)
        {
            return await appointmentsContext.Events
                .Include(e => e.Configuration)
                .ThenInclude(c => c!.Services)
                .Include(e => e.Configuration)
                .ThenInclude(c => c!.Attendes)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }
    }
}
