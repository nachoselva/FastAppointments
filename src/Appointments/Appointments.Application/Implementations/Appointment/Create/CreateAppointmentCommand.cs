namespace Appointments.Application.Implementations.Appointment.Create
{
    using Common.Application.CQRS;

    public sealed record CreateAppointmentCommand(
        bool IsRecurrent,
        DateTime StartOn,
        int? EventsCount,
        CreateAppointmentConfigurationCommand Configuration) : ICommand<Guid>;
}
