namespace backend.src.Application.Models.Common.Pagination
{
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {
    }
}
