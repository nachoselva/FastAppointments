namespace Common.Application.CQRS
{
    using FluentResults;
    using System.Threading.Tasks;

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
