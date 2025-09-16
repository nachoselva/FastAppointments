namespace Appointments.Application.Abstractions
{
    using Appointments.Domain.Entities;
    using System.Threading.Tasks;

    public interface IRecurrenceRepository
    {
        Task AddAsync(Recurrence recurrence);
    }
}
