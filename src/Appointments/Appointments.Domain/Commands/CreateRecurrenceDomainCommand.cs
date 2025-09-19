namespace Appointments.Domain.Commands
{
    internal record CreateRecurrenceDomainCommand(DateTime StartOn, int? EventsCount, CreateConfigurationDomainCommand Configuration);
}
