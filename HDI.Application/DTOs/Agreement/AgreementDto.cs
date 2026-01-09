namespace HDI.Application.DTOs.Agreement;

public class AgreementDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal RiskLimit { get; set; }
}

public class CreateAgreementRequest
{
    public string Title { get; set; } = string.Empty;
    public decimal RiskLimit { get; set; }
}

public class UpdateAgreementRequest
{
    public string Title { get; set; } = string.Empty;
    public decimal RiskLimit { get; set; }
}