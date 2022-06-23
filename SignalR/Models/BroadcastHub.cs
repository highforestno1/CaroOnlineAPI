
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Models
{
    public class BroadcastHub : Hub<IHubClient>
    {
        public string GetConnectionId() => Context.ConnectionId;
    }
}