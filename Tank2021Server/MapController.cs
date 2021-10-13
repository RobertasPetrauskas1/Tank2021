using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Tank2021.Hubs;
using Tank2021SharedContent;

namespace Tank2021Server
{
    public class MapController
    {
        public Map Map { get; set; }
        IHubContext<TankHub> hubContext;
        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second

        public MapController(IHubContext<TankHub> hubContext)
        {
            Map = new Map();
            this.hubContext = hubContext;
            timer.Interval = timerSpeed;
            timer.Start();

            AddMapSend();
        }
        private void AddMapSend()
        {
            timer.Elapsed += async (Object source, ElapsedEventArgs e) =>
            {
                await hubContext.Clients.All.SendAsync("ReceiveMessage", Map.ToJson());
            };
        }
    }
}
