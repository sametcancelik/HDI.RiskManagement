namespace HDI.Application.Interfaces;

public interface ICurrentTenantService
{
    int? TenantId { get; set; }
    int? GetTenantId();
    string? GetUsername();
}
