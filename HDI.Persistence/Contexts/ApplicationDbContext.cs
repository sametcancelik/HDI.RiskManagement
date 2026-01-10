using System.Linq.Expressions;
using HDI.Application.Interfaces;
using HDI.Domain.Common;
using HDI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HDI.Domain.Common.Helpers;

namespace HDI.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentTenantService _currentTenantService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentTenantService currentTenantService)
        : base(options)
    {
        _currentTenantService = currentTenantService;
    }

    public DbSet<Partner> Partners => Set<Partner>();
    public DbSet<Agreement> Agreements => Set<Agreement>();
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<WorkItem> WorkItems => Set<WorkItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetTenantFilter(entityType.ClrType));

            if (typeof(IAuditBaseEntity).IsAssignableFrom(entityType.ClrType))
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetSoftDeleteFilter(entityType.ClrType));
        }

        var seedDate = new DateTime(2024, 1, 1);
        var hashedSecret = HashHelper.ComputeSha256Hash("hdi-secret-789");

        modelBuilder.Entity<Partner>().HasData(
            new Partner
            {
                Id = 1,
                Name = "HDI Sigorta A.Ş.",
                ApiKey = "hdi-test-key-123",
                ApiSecret = hashedSecret,
                IsActive = true,
                CreatedBy = "System",
                CreatedDate = seedDate
            }
        );

        modelBuilder.Entity<Agreement>().HasData(
            new Agreement
            {
                Id = 1,
                TenantId = 1,
                Title = "Kasko Risk Analizi",
                RiskLimit = 150,
                CreatedBy = "System",
                CreatedDate = seedDate
            }
        );

        modelBuilder.Entity<Keyword>().HasData(
            new Keyword
            {
                Id = 1,
                TenantId = 1,
                AgreementId = 1,
                Word = "kaza",
                RiskWeight = 80,
                CreatedBy = "System",
                CreatedDate = seedDate
            },
            new Keyword
            {
                Id = 2,
                TenantId = 1,
                AgreementId = 1,
                Word = "sel",
                RiskWeight = 40,
                CreatedBy = "System",
                CreatedDate = seedDate
            }
        );

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<IAuditBaseEntity>();
        var currentTenantId = _currentTenantService.GetTenantId();
        var currentUser = _currentTenantService.GetUsername() ?? "System";

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUser;
                    entry.Entity.CreatedDate = DateTime.Now;

                    if (entry.Entity is ITenantEntity tenantEntity && currentTenantId.HasValue)
                    {
                        tenantEntity.TenantId = currentTenantId.Value;
                    }
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedBy = currentUser;
                    entry.Entity.ModifiedDate = DateTime.Now;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedBy = currentUser;
                    entry.Entity.DeletedDate = DateTime.Now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private LambdaExpression GetTenantFilter(Type type)
    {
        var parameter = Expression.Parameter(type, "e");
        var left = Expression.Property(parameter, nameof(ITenantEntity.TenantId));
        var tenantIdProperty = Expression.Property(Expression.Constant(_currentTenantService),
            nameof(ICurrentTenantService.TenantId));
        var leftConverted = Expression.Convert(left, typeof(int?));
        var comparison = Expression.Equal(leftConverted, tenantIdProperty);

        return Expression.Lambda(comparison, parameter);
    }

    private LambdaExpression GetSoftDeleteFilter(Type type)
    {
        var parameter = Expression.Parameter(type, "e");
        var property = Expression.Property(parameter, nameof(IAuditBaseEntity.IsDeleted));
        var falseConstant = Expression.Constant(false);
        var comparison = Expression.Equal(property, falseConstant);

        return Expression.Lambda(comparison, parameter);
    }
}