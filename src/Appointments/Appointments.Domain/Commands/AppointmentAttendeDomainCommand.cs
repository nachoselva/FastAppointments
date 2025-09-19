namespace Appointments.Domain.Commands
{
    using Common.Models.Enums;

    public sealed record AppointmentAttendeDomainCommand(AttendeCategory Category, AttendeType Type, Guid ExternalId, bool IsOptional);
}
