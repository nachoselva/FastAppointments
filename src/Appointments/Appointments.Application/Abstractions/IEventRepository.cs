namespace Appointments.Application.Abstractions
{
    using Appointments.Domain.Entities;
    using System.Threading.Tasks;

    public interface IEventRepository
    {
        Task AddAsync(Event @event);
    }
}
