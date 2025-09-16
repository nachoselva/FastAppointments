namespace Appointments.Domain.Commands
{
    internal record CreateRecurrenceDomainCommand(DateTime StartOn, int? EventsCount, string Description, int DurationInMinutes);
}
