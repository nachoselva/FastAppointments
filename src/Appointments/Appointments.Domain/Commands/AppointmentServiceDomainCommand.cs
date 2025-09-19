namespace Appointments.Domain.Commands
{
    public sealed record AppointmentServiceDomainCommand(Guid ExternalServiceId, int UnitsCount);
}
