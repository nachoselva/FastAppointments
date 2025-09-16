namespace Payments.Application.Bill.Create
{
    using Appointments.Application.Abstractions;
    using Appointments.Domain.Commands;
    using Appointments.Domain.Entities;
    using Common.Application.CQRS;
    using Common.Application.Repositories;
    using FluentResults;
    using Payments.Application.Implementations.Bill.Create;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IRecurrenceRepository _recurrenceRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEventPublisher<CreateBillEventBody> _billEventPublisher;

        public CreateAppointmentCommandHandler(IRecurrenceRepository recurrenceRepository, IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _recurrenceRepository = recurrenceRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> HandleAsync(CreateAppointmentCommand command, CancellationToken cancellationToken)
        {
            var domainCommand = new CreateAppointmentDomainCommand(command.IsRecurrent, command.StartOn, command.EventsCount, command.Description, command.DurationInMinutes);

            var (recurrence, @event) = AppointmentFactory.Create(domainCommand);

            if (recurrence != null)
                await _recurrenceRepository.AddAsync(recurrence);

            if (@event != null)
                await _eventRepository.AddAsync(@event);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return recurrence?.Id ?? @event!.Id;
        }
    }
}
