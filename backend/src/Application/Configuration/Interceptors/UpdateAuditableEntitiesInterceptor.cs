using backend.src.Application.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace Application.Configuration.Interceptors
{
    public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateAuditableEntitiesInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var user = _httpContextAccessor.HttpContext?.User;
            var currentUserId = user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "system";

            foreach (var entry in dbContext.ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationTime = DateTime.UtcNow;
                        entry.Entity.CreatorUserId = currentUserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModificationTime = DateTime.UtcNow;
                        entry.Entity.LastModifierUserId = currentUserId;
                        break;
                }
            }

            foreach (var entry in dbContext.ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.State == EntityState.Modified && entry.Entity.IsDeleted == 1)
                {
                    if (entry.Entity is IFullAudited fullAudited)
                    {
                        fullAudited.DeletionTime = DateTime.UtcNow;
                        fullAudited.DeleterUserId = currentUserId;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
