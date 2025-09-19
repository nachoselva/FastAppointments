namespace Appointments.Infrastructure.Implementations
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Entities;
    using Appointments.Infrastructure.Context;
    using System.Threading.Tasks;

    internal class RecurrenceRepository(AppointmentsContext appointmentsContext) : IRecurrenceRepository
    {
        public async Task AddAsync(Recurrence recurrence)
        {
            await appointmentsContext.Recurrences.AddAsync(recurrence);
        }
    }
}
