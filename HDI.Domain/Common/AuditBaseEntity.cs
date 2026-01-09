namespace HDI.Domain.Common;

public abstract class AuditBaseEntity<TId> : BaseEntity<TId>, IAuditBaseEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}