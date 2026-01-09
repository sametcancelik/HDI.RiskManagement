namespace HDI.Application.DTOs.Keyword;

public class KeywordDto
{
    public int Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public decimal RiskWeight { get; set; }
    public int AgreementId { get; set; }
}

public class CreateKeywordRequest
{
    public string Word { get; set; } = string.Empty;
    public decimal RiskWeight { get; set; }
    public int AgreementId { get; set; }
}
