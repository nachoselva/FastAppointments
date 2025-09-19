namespace Appointments.Application.Implementations.Appointment.Create
{
    public sealed record CreateAppointmentRequest(
        bool IsRecurrent,
        DateTime StartOn,
        int? EventsCount,
        CreateAppointmentConfigurationRequest Configuration);
}
