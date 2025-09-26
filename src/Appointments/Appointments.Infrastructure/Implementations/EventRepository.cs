namespace Appointments.Infrastructure.Implementations
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Entities;
    using Appointments.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
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

        public async Task<IEnumerable<Event>> GetAsync(DateTime? scheduledFrom, DateTime? scheduledTo)
        {
            var query = appointmentsContext.Events
                .Include(e => e.Configuration)
                .ThenInclude(c => c!.Services)
                .Include(e => e.Configuration)
                .ThenInclude(c => c!.Attendes)
                .AsQueryable();

            if(scheduledFrom.HasValue)
                query = query.Where(e => e.StartOn >= scheduledFrom.Value); 

            if(scheduledTo.HasValue)
                query = query.Where(e => e.StartOn.AddMinutes((e.Configuration ?? e.Recurrence!.Configuration)!.DurationInMinutes) <= scheduledTo.Value);

            return await query.ToListAsync();
        }
    }
}
