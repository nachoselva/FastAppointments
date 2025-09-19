namespace Appointments.Api.Controllers
{
    using Appointments.Application.Implementations.Events.Get;
    using Common.Application.CQRS;
    using Common.Models.Appointments;
    using FluentResults;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class EventsController(IQueryDispatcher queryDispatcher) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public Task<Result<GetEventResponse>> GetEvent(Guid id, CancellationToken cancellationToken)
        {
            return queryDispatcher.DispatchAsync<GetEventQuery, GetEventResponse>(new GetEventQuery(id), cancellationToken);
        }
    }
}
