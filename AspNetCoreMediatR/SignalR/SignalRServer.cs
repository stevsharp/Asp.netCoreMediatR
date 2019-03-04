using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AspNetCoreMediatR.SignalR
{
    public class SignalRServer : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }
    }
}
