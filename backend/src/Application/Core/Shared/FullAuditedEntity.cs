using System.ComponentModel.DataAnnotations.Schema;

namespace backend.src.Application.Core.Shared
{
    public class FullAuditedEntity : AuditedEntity, IFullAudited
    {
        public DateTime? DeletionTime { get; set; }
        public string? DeleterUserId { get; set; }
    }
}