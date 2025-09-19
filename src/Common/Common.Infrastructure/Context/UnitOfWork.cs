namespace Common.Infrastructure.Context
{
    using Common.Application.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
