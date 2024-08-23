using Microsoft.AspNetCore.SignalR;

namespace Media.API.NotificationHub
{
    public class LoginHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("RecieveLoginStatus", $"{Context.ConnectionId} has joined");
        }

        public async Task SendLoginStatus(string status)
        {
            await Clients.All.SendAsync("RecieveloginStatus", $"{status}");
        }
    }
}
