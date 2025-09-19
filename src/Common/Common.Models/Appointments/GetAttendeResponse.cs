namespace Common.Models.Appointments
{
    using Common.Models.Enums;

    public record GetAttendeResponse(AttendeCategory Category, AttendeType Type, Guid ExternalId, bool IsOptional);
}
