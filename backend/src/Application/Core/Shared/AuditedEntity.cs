namespace backend.src.Application.Core.Shared
{
    public class AuditedEntity : IAuditableEntity
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? CreatorUserId { get; set; }
        public string? LastModifierUserId { get; set; }
    }
}
