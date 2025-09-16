using Common.Application.CQRS;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Implementations.Bill.Create;

namespace Appointments.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    public AppointmentsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<Result<Guid>> CreateAppointment([FromBody] CreateAppointmentRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAppointmentCommand(
            request.IsRecurrent,
            request.StartOn,
            request.EventsCount,
            request.Description,
            request.DurationInMinutes);

        return await _commandDispatcher.DispatchAsync<CreateAppointmentCommand, Guid>(command, cancellationToken);
    }
}
