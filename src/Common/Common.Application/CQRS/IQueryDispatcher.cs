namespace Common.Application.CQRS
{
    using FluentResults;
    using System.Threading.Tasks;

    public interface IQueryDispatcher
    {
        Task<Result<TResult>> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery<TResult>;
    }
}
