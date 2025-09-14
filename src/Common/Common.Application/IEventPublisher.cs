namespace Payments.Application.Abstractions
{
    using System.Threading.Tasks;

    public interface IEventPublisher<T>
    {
        Task PublishAsync(T eventToBePublished, CancellationToken cancellationToken);
    }
}
