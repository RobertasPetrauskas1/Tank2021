using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Tank2021.Hubs
{
    public class TankHub : Hub
    {
        public async Task SendMovement(string word)
        {
            await Clients.All.SendAsync("ReceivedMovement", word + 1);
        }

        public async Task SendMessage(string user, string function, string[] args)
        {
            StringBuilder argString = new StringBuilder("");
            for(int i = 0; i < args.Length; i++)
            {
                argString.Append(args[i]);
                if(i != args.Length - 1)
                {
                    argString.Append(", ");
                }
            }
            Console.WriteLine($"{user}: {function}({argString})");
            await Clients.All.SendAsync("ReceiveMessage", user, function, args);
        }
    }
}
