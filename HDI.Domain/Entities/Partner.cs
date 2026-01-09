using HDI.Domain.Common;

namespace HDI.Domain.Entities;

public class Partner : AuditBaseEntity<int> 
{
    public string Name { get; set; } = string.Empty;
    public string ApiKey { get; set; } = Guid.NewGuid().ToString();
    public bool IsActive { get; set; }
    public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
}
