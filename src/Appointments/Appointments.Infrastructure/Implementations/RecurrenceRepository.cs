namespace Appointments.Infrastructure.Implementations
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Entities;
    using Appointments.Infrastructure.Context;
    using System.Threading.Tasks;

    internal class RecurrenceRepository : IRecurrenceRepository
    {
        private readonly AppointmentsContext _appointmentsContext;

        public RecurrenceRepository(AppointmentsContext appointmentsContext)
        {
            _appointmentsContext = appointmentsContext;
        }

        public async Task AddAsync(Recurrence recurrence)
        {
            await _appointmentsContext.Recurrences.AddAsync(recurrence);
        }
    }
}
