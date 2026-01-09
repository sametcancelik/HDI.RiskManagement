using HDI.Domain.Common;

namespace HDI.Domain.Entities;

public class WorkItem : AuditBaseEntity<int>, ITenantEntity
{
    public string Description { get; set; } = string.Empty;
    public decimal CalculatedRiskAmount { get; set; }
    public bool IsLimitExceeded { get; set; }
    
    public int AgreementId { get; set; }
    public int TenantId { get; set; } 

    public virtual Agreement Agreement { get; set; } = null!;
}
