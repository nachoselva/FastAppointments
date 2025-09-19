namespace Appointments.Application.Implementations.Appointment.Create
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Commands;
    using Appointments.Domain.Entities;
    using Common.Application.CQRS;
    using Common.Application.Repositories;
    using Common.Models.Payments;
    using FluentResults;
    using Payments.Application.Abstractions;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CreateAppointmentCommandHandler(
        IRecurrenceRepository recurrenceRepository,
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        IEventPublisher<PendingBillEventBody> billEventPublisher) : ICommandHandler<CreateAppointmentCommand, Guid>
    {
        public async Task<Result<Guid>> HandleAsync(CreateAppointmentCommand command, CancellationToken cancellationToken)
        {
            var domainCommand = new CreateAppointmentDomainCommand(
                command.IsRecurrent,
                command.StartOn,
                command.EventsCount,
                new CreateConfigurationDomainCommand(
                    command.Configuration.Description, 
                    command.Configuration.DurationInMinutes, 
                    command.Configuration.Services.Select(s => new AppointmentServiceDomainCommand(s.ExternalServiceId, s.UnitsCount)),
                    command.Configuration.Attendes.Select(a => new AppointmentAttendeDomainCommand(a.Category, a.Type, a.ExternalId, a.IsOptional))));

            var (recurrence, @event) = AppointmentFactory.Create(domainCommand);

            if (recurrence != null)
                await recurrenceRepository.AddAsync(recurrence);

            if (@event != null)
                await eventRepository.AddAsync(@event);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            List<Event> eventToPublish = [];
            if (recurrence?.Events != null)
                eventToPublish.AddRange(recurrence.Events);
            if (@event != null)
                eventToPublish.Add(@event);
            
            await billEventPublisher.PublishAsync(eventToPublish.Select(ev => new PendingBillEventBody("Event", ev.Id)), cancellationToken);

            return recurrence?.Id ?? @event!.Id;
        }
    }
}
