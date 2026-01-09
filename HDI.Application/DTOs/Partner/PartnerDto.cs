namespace HDI.Application.DTOs.Partner;

public class PartnerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class CreatePartnerRequest
{
    public string Name { get; set; } = string.Empty;
}
