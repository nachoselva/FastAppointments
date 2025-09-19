namespace Payments.Application.Abstractions
{
    using System.Threading.Tasks;

    public interface IEventPublisher<T>
    {
        Task PublishAsync(T eventToBePublished, CancellationToken cancellationToken);
        Task PublishAsync(IEnumerable<T> eventToBePublished, CancellationToken cancellationToken);
    }
}
