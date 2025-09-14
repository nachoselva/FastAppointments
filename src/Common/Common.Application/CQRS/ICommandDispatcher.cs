namespace Common.Application.CQRS
{
    using FluentResults;
    using System.Threading.Tasks;

    public interface ICommandDispatcher
    {
        Task<Result<TResult>> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResult>;
    }
}
