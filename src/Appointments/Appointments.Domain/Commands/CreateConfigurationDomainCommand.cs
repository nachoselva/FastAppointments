namespace Appointments.Domain.Commands
{
    public record CreateConfigurationDomainCommand(string Description, int DurationInMinutes, 
        IEnumerable<AppointmentServiceDomainCommand> Services,
        IEnumerable<AppointmentAttendeDomainCommand> Attendes);

}
