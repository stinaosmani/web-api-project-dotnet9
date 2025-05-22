namespace backend.src.Application.Models.Common.Pagination
{
    public interface IHasTotalCount
    {
        int TotalCount { get; set; }
    }
}
