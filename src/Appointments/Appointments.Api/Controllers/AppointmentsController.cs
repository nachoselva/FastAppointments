namespace Appointments.Api.Controllers
{
    using Appointments.Application.Implementations.Appointment.Create;
    using Common.Application.CQRS;
    using FluentResults;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController(ICommandDispatcher commandDispatcher) : ControllerBase
    {
        [HttpPost]
        public Task<Result<Guid>> CreateAppointment([FromBody] CreateAppointmentRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateAppointmentCommand(
                request.IsRecurrent,
                request.StartOn,
                request.EventsCount,
                new CreateAppointmentConfigurationCommand(
                    request.Configuration.Description,
                    request.Configuration.DurationInMinutes,
                    request.Configuration.ExternalLocationId,
                    request.Configuration.Services.Select(s => new AppointmentServiceCommand(s.ExternalServiceId, s.UnitsCount)),
                    request.Configuration.Attendes.Select(a => new AppointmentAttendeCommand(a.Category, a.Type, a.ExternalId, a.IsOptional))
                    ));

            return commandDispatcher.DispatchAsync<CreateAppointmentCommand, Guid>(command, cancellationToken);
        }
    }
}