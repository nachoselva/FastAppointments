namespace Common.Application.CQRS
{
    public interface ICommandValidator<TCommand, TResult> : IValidator<TCommand> where TCommand : ICommand<TResult>;
}
