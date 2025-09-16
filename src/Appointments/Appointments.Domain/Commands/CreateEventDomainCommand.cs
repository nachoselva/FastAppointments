namespace Appointments.Domain.Commands
{
    public record CreateEventDomainCommand(DateTime StartOn, CreateConfigurationDomainCommand? Configuration = null);
}
