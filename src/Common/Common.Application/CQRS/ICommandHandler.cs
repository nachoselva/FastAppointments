namespace Common.Application.CQRS
{
    using FluentResults;
    using System.Threading.Tasks;

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
