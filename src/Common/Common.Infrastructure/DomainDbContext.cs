namespace Common.Infrastructure
{
    using Common.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    public abstract class DomainDbContext : DbContext
    {
        protected DomainDbContext(DbContextOptions options) : base(options)
        {

        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<DomainEntity> entry in ChangeTracker.Entries<DomainEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Property("ModifiedOn").CurrentValue = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Property("ModifiedOn").CurrentValue = DateTime.UtcNow;
                        entry.Property("IsDeleted").CurrentValue = true;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Type implementingDbContextType = this.GetType();

            Assembly configurationsAssembly = implementingDbContextType.Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly);

            var domainEntityConfiguration = new DomainEntityConfiguration();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(DomainEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var builder = modelBuilder.Entity(entityType.ClrType);

                    MethodInfo processMethod = typeof(DomainEntityConfiguration)
                        .GetMethod(nameof(DomainEntityConfiguration.Configure))!;

                    MethodInfo genericProcessMethod = processMethod
                        .MakeGenericMethod(entityType.ClrType);

                    genericProcessMethod.Invoke(domainEntityConfiguration, [builder]);

                    foreach (var property in entityType.GetProperties())
                    {
                        if (property.ClrType == typeof(decimal))
                        {
                            builder.Property(property.Name).HasPrecision(18, 2);
                        }
                        else if (property.ClrType == typeof(string))
                        {
                            builder.Property(property.Name).HasMaxLength(200);
                        }
                    }
                }
            }

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
