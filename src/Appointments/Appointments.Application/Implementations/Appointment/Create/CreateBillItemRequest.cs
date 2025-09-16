namespace Payments.Application.Implementations.Bill.Create
{
    public sealed record CreateAppointmentRequest(bool IsRecurrent, DateTime StartOn, int? EventsCount, string Description, int DurationInMinutes);
}
