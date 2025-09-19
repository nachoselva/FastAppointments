namespace Common.Models.Appointments
{
    public record GetEventResponse(
        Guid Id,
        DateTime StartOn,
        string Description,
        int DurationInMinutes,
        IEnumerable<GetExternalServiceResponse> Services,
        IEnumerable<GetAttendeResponse> Attendes);
}
