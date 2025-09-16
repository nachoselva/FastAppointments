namespace FluentResults
{
    using System.Collections.Generic;

    public static class FluentResultExtensions
    {
        public static Result<IEnumerable<TOutput>> BulkResult<TInput, TOutput>(this IEnumerable<TInput> commands, Func<TInput, Result<TOutput>> rowMap)
            where TOutput : class
        {
            var results = commands.Select(rowMap);
            return Result.Merge([.. results]);
        }
    }
}
