namespace Appointments.Infrastructure.Implementations
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Entities;
    using Appointments.Infrastructure.Context;
    using System.Threading.Tasks;

    internal class EventRepository : IEventRepository
    {
        private readonly AppointmentsContext _appointmentsContext;

        public EventRepository(AppointmentsContext appointmentsContext)
        {
            _appointmentsContext = appointmentsContext;
        }

        public async Task AddAsync(Event Event)
        {
            await _appointmentsContext.Events.AddAsync(Event);
        }
    }
}
