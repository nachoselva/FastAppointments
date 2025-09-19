namespace Appointments.Application.Implementations.Appointment.Create
{
    using Common.Models.Enums;

    public sealed record AppointmentAttendeCommand(AttendeCategory Category, AttendeType Type, Guid ExternalId, bool IsOptional);
}
