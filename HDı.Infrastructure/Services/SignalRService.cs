using Microsoft.AspNetCore.SignalR;
using HDI.Application.Interfaces;
using HDı.Infrastructure.Hubs;

namespace HDI.Infrastructure.Services;

public class SignalRService(IHubContext<RiskHub> hubContext) : ISignalRService
{
    private readonly IHubContext<RiskHub> _hubContext = hubContext;

    public async Task SendRiskAlertAsync(string tenantId, object data)
    {
        if (string.IsNullOrEmpty(tenantId)) return;

        await _hubContext.Clients.Group(tenantId).SendAsync("ReceiveRiskAlert", data);
    }
}