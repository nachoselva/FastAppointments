namespace Payments.Application.Implementations.Bill.Create
{
    using Common.Application.CQRS;

    public sealed record CreateAppointmentCommand(bool IsRecurrent, DateTime StartOn, int? EventsCount, string Description, int DurationInMinutes) : ICommand<Guid>;
}
