namespace Appointments.Domain.Commands
{
    public record CreateConfigurationDomainCommand(
        string Description, 
        int DurationInMinutes, 
        Guid ExternalLocationId,
        IEnumerable<AppointmentServiceDomainCommand> Services,
        IEnumerable<AppointmentAttendeDomainCommand> Attendes);

}
