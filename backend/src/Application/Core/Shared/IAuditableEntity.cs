namespace backend.src.Application.Core.Shared
{
    public interface IAuditableEntity
    {
        DateTime CreationTime { get; set; }
        DateTime? LastModificationTime { get; set; }
        string? CreatorUserId { get; set; }
        string? LastModifierUserId { get; set; }
    }
}