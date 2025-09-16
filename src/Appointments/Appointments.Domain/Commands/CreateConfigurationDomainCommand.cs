namespace Appointments.Domain.Commands
{
    public record CreateConfigurationDomainCommand(string Description, int DurationInMinutes);
}
