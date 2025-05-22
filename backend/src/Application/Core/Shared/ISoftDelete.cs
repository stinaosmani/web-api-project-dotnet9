namespace backend.src.Application.Core.Shared
{
    public interface ISoftDelete
    {
        int IsDeleted { get; set; } // 0 = not deleted, 1 = deleted
        DateTime? DeletionTime { get; set; }
        string? DeleterUserId { get; set; }
    }
}