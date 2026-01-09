namespace HDI.Application.DTOs.Dashboard;

public class TenantDashboardDto
{
    public int TotalAnalysisCount { get; set; }
    public int HighRiskCount { get; set; }
    public double HighRiskPercentage { get; set; }
    public int AverageRiskScore { get; set; }
    public List<TopRiskKeywordDto> MostTriggeredKeywords { get; set; } = new();
}

public class TopRiskKeywordDto
{
    public string Word { get; set; } = string.Empty;
    public int Count { get; set; }
}