namespace Appointments.Domain.Commands
{
    public record CreateAppointmentDomainCommand(bool IsRecurrent, DateTime StartOn, int? EventsCount, string Description, int DurationInMinutes);
}
