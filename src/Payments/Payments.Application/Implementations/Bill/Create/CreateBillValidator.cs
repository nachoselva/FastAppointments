namespace Payments.Application.Implementations.Bill.Create
{
    using Common.Application.CQRS;
    using FluentResults;
    using Payments.Application.Implementations.PaymentEntity.Common;
    using System.Threading.Tasks;

    internal class CreateBillValidator : ICommandValidator<CreateBillCommand, Guid>
    {
        public Task<Result> ValidateAsync(CreateBillCommand command)
        {
            var senderResult = Validate(command.Sender);
            var receiverResult = Validate(command.Receiver);

            return Task.FromResult(Result.Merge(senderResult, receiverResult));
        }

        private static Result Validate(PaymentEntityCommand sender)
        {
            if (sender.ClientId == null && sender.ProviderId == null && sender.CompanyId == null)
                return Result.Fail("External Ids can't be empty");
            return Result.Ok(); 
        }
    }
}
