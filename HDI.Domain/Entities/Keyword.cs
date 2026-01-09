using HDI.Domain.Common;

namespace HDI.Domain.Entities;

public class Keyword : AuditBaseEntity<int>, ITenantEntity
{
    public string Word { get; set; } = string.Empty;
    public decimal RiskWeight { get; set; }
    
    public int AgreementId { get; set; }
    public int TenantId { get; set; }
    
    public virtual Agreement Agreement { get; set; } = null!;
}
