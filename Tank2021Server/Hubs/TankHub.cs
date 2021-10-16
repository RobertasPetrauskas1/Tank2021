using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Tank2021SharedContent;
using Tank2021SharedContent.Enums;

namespace Tank2021.Hubs
{
    public class TankHub : Hub
    {
        public async Task InitializeGame(PlayerType player)
        {
            var map = MapSingleton.getMapController();

            if(player == PlayerType.PLAYER1)
            {
                var player1 = map.GetPlayer(player);

                player1.Coins = 0;
                player1.Tank = new Tank(new Point(30, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png");
            }

            if(player == PlayerType.PLAYER2)
            {
                var player2 = map.GetPlayer(player);

                player2.Coins = 0;
                player2.Tank = new Tank(new Point(800, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png");
            }

            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task MoveDown(PlayerType player)
        {
            var map = MapSingleton.getMapController();
            map.MoveDown(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task MoveUp(PlayerType player)
        {
            var map = MapSingleton.getMapController();
            map.MoveUp(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task MoveLeft(PlayerType player)
        {
            var map = MapSingleton.getMapController();
            map.MoveLeft(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }

        public async Task MoveRight(PlayerType player)
        {
            var map = MapSingleton.getMapController();
            map.MoveRight(player);
            await Clients.All.SendAsync("UpdateMap", map.ToJson());
        }
    }
}
