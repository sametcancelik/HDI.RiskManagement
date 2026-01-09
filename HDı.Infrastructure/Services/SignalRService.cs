using HDI.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace HDI.Infrastructure.Services;
public class SignalRService(IHubContext<Hub> hubContext) : ISignalRService
{
    private readonly IHubContext<Hub> _hubContext = hubContext;

    public async Task SendRiskAlertAsync(string tenantId, object data)
    {
        await _hubContext.Clients.Group(tenantId).SendAsync("ReceiveRiskAlert", data);
    }
}