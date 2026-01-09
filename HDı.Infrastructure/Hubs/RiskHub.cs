using Microsoft.AspNetCore.SignalR;

namespace HDı.Infrastructure.Hubs;

public class RiskHub : Hub 
{
    public override async Task OnConnectedAsync()
    {
        var tenantId = Context.GetHttpContext()?.Request.Query["tenantId"];
        if (!string.IsNullOrEmpty(tenantId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tenantId);
        }
        await base.OnConnectedAsync();
    }
}