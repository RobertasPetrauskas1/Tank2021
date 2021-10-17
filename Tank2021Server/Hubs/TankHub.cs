using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tank2021SharedContent;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Guns;

namespace Tank2021.Hubs
{
    public class TankHub : Hub
    {
        public Timer timer = new Timer();
        public static double timerSpeed = 36; //~30times per second
        bool IsGameInitialized = false;

        public async Task InitializeGame(PlayerType player)
        {
            var map = MapSingleton.getMap();

            if(player == PlayerType.PLAYER1)
            {
                var player1 = map.GetPlayer(player);

                player1.Coins = 0;
                player1.Tank = new Tank(new BaseGun(), new Point(30, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png");
            }

            if(player == PlayerType.PLAYER2)
            {
                var player2 = map.GetPlayer(player);

                player2.Coins = 0;
                player2.Tank = new Tank(new BaseGun(), new Point(800, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png");
            }

            await Clients.All.SendAsync("UpdateMap", map.ToJson());
            IsGameInitialized = true;
            AddGameCycle();
        }

        public async Task MoveDown(PlayerType player)
        {
            var map = MapSingleton.getMap();
            map.MoveDown(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task MoveUp(PlayerType player)
        {
            var map = MapSingleton.getMap();
            map.MoveUp(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task MoveLeft(PlayerType player)
        {
            var map = MapSingleton.getMap();
            map.MoveLeft(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task MoveRight(PlayerType player)
        {
            var map = MapSingleton.getMap();
            map.MoveRight(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task Shoot(PlayerType player)
        {
            var map = MapSingleton.getMap();
            map.Shoot(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }
        private void AddGameCycle()
        {
            timer.Elapsed += async (Object source, ElapsedEventArgs e) =>
            {
                var map = MapSingleton.getMap();
                foreach(var bullet in map.bullets)
                {
                    if (bullet.MarkForDelete)
                    {
                        map.bullets.Remove(bullet);
                    }
                    else
                    {
                        bullet.Move();
                    }            
                }
                await Clients.All.SendAsync("UpdateMap", map.ToJson());
            };
        }
    }
}
