namespace backend.src.Application.Core.Shared
{
    public interface ISoftDelete
    {
        DateTime? DeletionTime { get; set; }
        string? DeleterUserId { get; set; }
    }
}