using Microsoft.AspNetCore.SignalR;

namespace KupujemProdajemWebApp
{
    public class NotificationHub : Hub
    {
        public async Task SendWelcomeMessage(string username)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", $"{username} Welcome");
        }
    }
}
