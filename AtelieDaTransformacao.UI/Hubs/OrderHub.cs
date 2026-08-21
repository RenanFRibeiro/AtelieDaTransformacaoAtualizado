using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AtelieDaTransformacao.UI.Hubs;

[Authorize]
public sealed class OrderHub : Hub
{
}
