using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace KupujemProdajemWebApp
{
    public class NotificationHub : Hub
    {
        public async Task SendWelcomeMessage(string username)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", $"{username}");
        }

        public async Task SavedAdNotification(string ownerId, string message)
        {
            await Clients.User(ownerId).SendAsync("ReceiveNotification", message);
        }
    }
}
