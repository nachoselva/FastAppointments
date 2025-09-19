namespace Appointments.Application.Implementations.Appointment.Create
{
    public sealed record CreateAppointmentConfigurationRequest(
        string Description,
        int DurationInMinutes,
        IEnumerable<AppointmentServiceRequest> Services,
        IEnumerable<AppointmentAttendeRequest> Attendes);
}
