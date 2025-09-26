namespace Appointments.Application.Implementations.Events.Get
{
    using Common.Application.CQRS;
    using Common.Models.Appointments;

    public sealed record GetEventsQuery(DateTime? ScheduledFrom, DateTime? ScheduledTo) : IQuery<IEnumerable<GetEventResponse>>;
}
