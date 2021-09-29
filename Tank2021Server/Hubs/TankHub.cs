using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Tank2021.Hubs
{
    public class TankHub : Hub
    {
        public async Task SendMovement(string word)
        {
            await Clients.All.SendAsync("ReceivedMovement", word + 1);
        }
    }
}
