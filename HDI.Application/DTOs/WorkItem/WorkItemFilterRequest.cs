namespace HDI.Application.DTOs.WorkItem;

public class WorkItemFilterRequest
{
    public int? AgreementId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public bool? IsLimitExceeded { get; set; }
}
