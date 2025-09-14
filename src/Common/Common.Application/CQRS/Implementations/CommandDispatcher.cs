namespace Common.Application.CQRS.Implementations
{
    using FluentResults;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result<TResult>> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) 
            where TCommand : ICommand<TResult>
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            var validator = _serviceProvider.GetService<ICommandValidator<TCommand, TResult>>();
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
