namespace Common.Application.CQRS.Implementations
{
    using FluentResults;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    internal sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result<TResult>> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery<TResult>
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            var validator = _serviceProvider.GetService<IQueryValidator<TQuery, TResult>>();
            if (validator != null)
            {
                var validatorResult = await validator.ValidateAsync(query);
                if(validatorResult.IsFailed)
                    return Result.Fail(validatorResult.Errors);
            }
            return await handler.HandleAsync(query, cancellationToken);
        }
    }
}
