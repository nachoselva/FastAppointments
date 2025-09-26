namespace Appointments.Application.Implementations.Events.Get
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Entities;
    using Common.Application.CQRS;
    using Common.Models.Appointments;
    using FluentResults;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    internal class GetEventsQueryHandler(IEventRepository eventRepository) : IQueryHandler<GetEventsQuery, IEnumerable<GetEventResponse>>
    {
        public async Task<Result<IEnumerable<GetEventResponse>>> HandleAsync(GetEventsQuery query, CancellationToken cancellationToken)
        {
            var events = await eventRepository.GetAsync(query.ScheduledFrom, query.ScheduledTo);

            var results =  events.Select(e =>
            {
               var config = (e.Configuration ?? e.Recurrence!.Configuration)!;

               return new GetEventResponse(
               e.Id,
               e.StartOn,
               config.Description,
               config.DurationInMinutes,
               config.ExternalLocationId,
               config.Services.Select(s => new GetExternalServiceResponse(s.ExternalServiceId, s.UnitsCount)),
               config.Attendes.Select(a => new GetAttendeResponse(a.Category, a.Type, a.ExternalId, a.IsOptional)));
            });

            return Result.Ok(results);
        }
    }
}
