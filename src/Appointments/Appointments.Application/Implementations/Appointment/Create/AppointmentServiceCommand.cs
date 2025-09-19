namespace Appointments.Application.Implementations.Appointment.Create
{
    public sealed record AppointmentServiceCommand(Guid ExternalServiceId, int UnitsCount);
}
