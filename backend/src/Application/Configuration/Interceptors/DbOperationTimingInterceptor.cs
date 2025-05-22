using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Configuration.Interceptors
{
    public class DbOperationTimingInterceptor : SaveChangesInterceptor
    {
        private readonly ILogger<DbOperationTimingInterceptor> _logger;

        public DbOperationTimingInterceptor(ILogger<DbOperationTimingInterceptor> logger)
        {
            _logger = logger;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return await base.SavingChangesAsync(eventData, result, cancellationToken);

            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("[DB] SaveChanges started at {StartTime} for {Context}", DateTime.UtcNow, context.GetType().Name);

            var entries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            var added = new Dictionary<string, int>();
            var updated = new Dictionary<string, int>();
            var deleted = new Dictionary<string, int>();

            foreach (var entry in entries)
            {
                var entityName = entry.Entity.GetType().Name;

                if (entry.State == EntityState.Added)
                {
                    added[entityName] = added.GetValueOrDefault(entityName, 0) + 1;
                }
                else if (entry.State == EntityState.Modified)
                {
                    var isSoftDelete = entry.Properties.Any(p =>
                        p.Metadata.Name == "IsDeleted" &&
                        (p.OriginalValue?.ToString() == "0" || p.OriginalValue?.ToString()?.ToLower() == "false") &&
                        (p.CurrentValue?.ToString() == "1" || p.CurrentValue?.ToString()?.ToLower() == "true"));

                    if (isSoftDelete)
                        deleted[entityName] = deleted.GetValueOrDefault(entityName, 0) + 1;
                    else
                        updated[entityName] = updated.GetValueOrDefault(entityName, 0) + 1;
                }
            }

            // Logging grouped results
            foreach (var kv in added)
                _logger.LogInformation("Added: {Count} x {Entity}", kv.Value, kv.Key);

            foreach (var kv in updated)
                _logger.LogInformation("Updated: {Count} x {Entity}", kv.Value, kv.Key);

            foreach (var kv in deleted)
                _logger.LogInformation("Deleted: {Count} x {Entity}", kv.Value, kv.Key);

            var resultValue = await base.SavingChangesAsync(eventData, result, cancellationToken);

            stopwatch.Stop();
            _logger.LogInformation("[DB] SaveChanges completed at {EndTime} (Duration: {Duration}ms) for {Context}",
                DateTime.UtcNow, stopwatch.ElapsedMilliseconds, context.GetType().Name);

            return resultValue;
        }
    }
}
