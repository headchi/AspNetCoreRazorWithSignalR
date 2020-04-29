
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebApplicationSignalR.Hubs
{
    public class BroadCastHub : Hub
    {
        public Task Feedback(string message)
        {
            return Clients.Caller.SendAsync("feedBack", message);
        }
    }
}
