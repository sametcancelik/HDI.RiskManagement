using HDI.Domain.Common;

namespace HDI.Domain.Entities;

public class Agreement : AuditBaseEntity<int>, ITenantEntity
{
    public string Title { get; set; } = string.Empty;
    public decimal RiskLimit { get; set; }
    public int TenantId { get; set; } 
    
    public virtual ICollection<Keyword> Keywords { get; set; } = new List<Keyword>();
    public virtual ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}
