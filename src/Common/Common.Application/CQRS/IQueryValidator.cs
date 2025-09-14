namespace Common.Application.CQRS
{
    public interface IQueryValidator<TQuery, TResult> : IValidator<TQuery> where TQuery : IQuery<TResult>;
}
