namespace Appointments.Application.Implementations.Events.Get
{
    using Common.Application.CQRS;
    using Common.Models.Appointments;

    public sealed record GetEventQuery(Guid EventId) : IQuery<GetEventResponse>;
}
