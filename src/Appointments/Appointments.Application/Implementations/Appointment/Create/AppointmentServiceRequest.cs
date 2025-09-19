namespace Appointments.Application.Implementations.Appointment.Create
{
    public sealed record AppointmentServiceRequest(Guid ExternalServiceId, int UnitsCount);
}
