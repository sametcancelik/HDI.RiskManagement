namespace HDI.Application.Interfaces;

public interface ISignalRService
{
    Task SendRiskAlertAsync(string tenantId, object data);
}