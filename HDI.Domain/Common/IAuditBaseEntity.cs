namespace HDI.Domain.Common;

public interface IAuditBaseEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    string? ModifiedBy { get; set; }
    DateTime? ModifiedDate { get; set; }
    bool IsDeleted { get; set; }
    string? DeletedBy { get; set; }
    DateTime? DeletedDate { get; set; }
}
