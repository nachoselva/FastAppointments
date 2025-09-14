namespace Common.Application.CQRS
{
    using FluentResults;
    using System.Threading.Tasks;

    public interface IValidator<T>
    {
        Task<Result> ValidateAsync(T entity);
    }
}
