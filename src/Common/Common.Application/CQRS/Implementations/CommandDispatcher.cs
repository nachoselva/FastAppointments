namespace Common.Application.CQRS.Implementations
{
    using FluentResults;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    internal sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
    {
        public async Task<Result<TResult>> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) 
            where TCommand : ICommand<TResult>
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            var validator = serviceProvider.GetService<ICommandValidator<TCommand, TResult>>();
            if (validator != null)
            {
                var validatorResult = await validator.ValidateAsync(command);
                if (validatorResult.IsFailed)
                    return Result.Fail(validatorResult.Errors);
            }
            return await handler.HandleAsync(command, cancellationToken);
        }
    }
}
