using System.Threading.Tasks;

namespace SignalR.Models
{
    public interface IHubClient
    {
        Task BroadCastMessage();
        Task BroadcastNotification(NotificationMessageModel data);
    }
}