namespace HDI.Application.DTOs.WorkItem;

public class WorkItemDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal CalculatedRiskAmount { get; set; }
    public bool IsLimitExceeded { get; set; }
    public DateTime CreatedDate { get; set; }
    public int AgreementId { get; set; }
    public string AgreementTitle { get; set; } = string.Empty;
}
