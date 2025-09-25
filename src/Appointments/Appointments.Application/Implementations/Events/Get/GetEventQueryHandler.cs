namespace Appointments.Application.Implementations.Events.Get
{
    using Appointments.Application.Abstractions;
    using Common.Application.CQRS;
    using Common.Models.Appointments;
    using FluentResults;
    using System.Threading;
    using System.Threading.Tasks;

    internal class GetEventQueryHandler(IEventRepository eventRepository) : IQueryHandler<GetEventQuery, GetEventResponse>
    {
        public async Task<Result<GetEventResponse>> HandleAsync(GetEventQuery query, CancellationToken cancellationToken)
        {
            var @event = await eventRepository.GetAsync(query.EventId);

            if (@event == null)
                return Result.Fail("Event not found");

            var config = @event.Configuration ?? @event.Recurrence!.Configuration;

            if (config == null)
                return Result.Fail("Config not found");

            return new GetEventResponse(
                @event.Id,
                @event.StartOn,
                config.Description,
                config.DurationInMinutes,
                config.ExternalLocationId,
                config.Services.Select(s => new GetExternalServiceResponse(s.ExternalServiceId, s.UnitsCount)),
                config.Attendes.Select(a => new GetAttendeResponse(a.Category, a.Type, a.ExternalId, a.IsOptional)));
        }
    }
}
