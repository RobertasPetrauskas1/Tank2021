using Microsoft.AspNetCore.SignalR;
using System;
using System.Timers;
using Tank2021.Hubs;
using Tank2021SharedContent;
using Tank2021SharedContent.Enums;

namespace Tank2021Server
{
    public class MapController
    {
        public Map Map { get; set; }
        IHubContext<TankHub> hubContext;
        public Timer timer = new Timer();
        public static double timerSpeed = 30;

        public MapController(IHubContext<TankHub> hubContext)
        {
            Map = new Map();
            this.hubContext = hubContext;
            timer.Interval = timerSpeed;
            timer.Enabled = true;
            timer.Start();

            AddMapSend();
            AddBulletMovement();
        }
        private void AddMapSend()
        {
            timer.Elapsed += async (Object source, ElapsedEventArgs e) =>
            {
                await hubContext.Clients.All.SendAsync("UpdateMap", Map.ToJson());
            };
        }

        private void AddBulletMovement()
        {
            timer.Elapsed += (Object source, ElapsedEventArgs e) =>
            {
                var mapController = MapControllerSingleton.getMapController();

                var player1Gun = mapController.Map.GetPlayer(PlayerType.PLAYER1)?.Tank?.Gun;
                if(player1Gun != null)
                    for(var index = player1Gun.Bullets.Count - 1; index > -1; index--)
                    {
                        if (player1Gun.Bullets[index].MarkForDelete)
                            player1Gun.Bullets.RemoveAt(index);
                        else
                            player1Gun.Bullets[index].Move();
                    }

                var player2Gun = mapController.Map.GetPlayer(PlayerType.PLAYER2)?.Tank?.Gun;
                if(player2Gun != null)
                    for (var index = player2Gun.Bullets.Count - 1; index > -1; index--)
                    {
                        if (player2Gun.Bullets[index].MarkForDelete)
                            player2Gun.Bullets.RemoveAt(index);
                        else
                            player2Gun.Bullets[index].Move();
                    }
            };
        }
    }
}
