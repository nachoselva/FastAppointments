namespace Appointments.Application.Implementations.Appointment.Create
{
    public sealed record CreateAppointmentConfigurationCommand(
        string Description,
        int DurationInMinutes,
        IEnumerable<AppointmentServiceCommand> Services,
        IEnumerable<AppointmentAttendeCommand> Attendes);
}
