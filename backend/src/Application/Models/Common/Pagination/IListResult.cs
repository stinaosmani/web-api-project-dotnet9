namespace backend.src.Application.Models.Common.Pagination
{
    public interface IListResult<T>
    {
        //
        // Summary:
        //     List of items.
        IReadOnlyList<T> Items { get; set; }
    }
}
