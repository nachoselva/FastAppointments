namespace Appointments.Application.Implementations.Appointment.Create
{
    public sealed record CreateAppointmentConfigurationCommand(
        string Description,
        int DurationInMinutes,
        Guid ExternalLocationId,
        IEnumerable<AppointmentServiceCommand> Services,
        IEnumerable<AppointmentAttendeCommand> Attendes);
}
