using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AtelieDaTransformacao.UI.Hubs;

[Authorize]
public sealed class OrderStatusHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrWhiteSpace(userId))
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName(userId));

        if (Context.User?.IsInRole("Admin") == true)
            await Groups.AddToGroupAsync(Context.ConnectionId, AdminGroupName);

        await base.OnConnectedAsync();
    }

    public static string GroupName(string userId) => $"order-user-{userId}";
    public const string AdminGroupName = "order-admins";
}
