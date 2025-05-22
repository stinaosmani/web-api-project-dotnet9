using System.ComponentModel.DataAnnotations;

namespace backend.src.Application.Models.Common.Pagination
{
    public class PagedResultRequestDto
    {
        public static int DefaultMaxResultCount { get; set; } = 10;

        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }

        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = DefaultMaxResultCount;
    }
}
