using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tank2021Server;
using Tank2021SharedContent;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Guns;

namespace Tank2021.Hubs
{
    public class TankHub : Hub
    {
        public async Task InitializeGame(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();

            if(player == PlayerType.PLAYER1)
            {
                var player1 = mapController.Map.GetPlayer(player);

                player1.Coins = 0;
                player1.Tank = new Tank(new BaseGun(), new Point(30, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png", 100);
            }

            if(player == PlayerType.PLAYER2)
            {
                var player2 = mapController.Map.GetPlayer(player);

                player2.Coins = 0;
                player2.Tank = new Tank(new BaseGun(), new Point(800, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png", 100);
            }

            await Clients.All.SendAsync("InitializeGame", mapController.Map.ToJson());
        }

        public async Task MoveDown(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.MoveDown(player);
            await Clients.All.SendAsync("UpdateMap", mapController.Map.ToJson());
        }

        public async Task MoveUp(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.MoveUp(player);
            await Clients.All.SendAsync("UpdateMap", mapController.Map.ToJson());
        }

        public async Task MoveLeft(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.MoveLeft(player);
            await Clients.All.SendAsync("UpdateMap", mapController.Map.ToJson());
        }

        public async Task MoveRight(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.MoveRight(player);
            await Clients.All.SendAsync("UpdateMap", mapController.Map.ToJson());
        }

        public async Task Shoot(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.Shoot(player);
            await Clients.All.SendAsync("UpdateMap", mapController.Map.ToJson());
        }
    }
}
