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
        public async Task ConnectPlayer(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.GetPlayer(player).IsConnected = true;
            InitializePlayer(player);
            if (mapController.Map.GetOppositePlayer(player).IsConnected)
            {
                await InitializeGame();
            } 
        }

        public async Task DisconnectPlayer(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.GetPlayer(player).IsConnected = false; //Do we really need this?
            mapController.ResetGame();

            await Clients.All.SendAsync("GameOver", Helper.GetOppositePlayer(player));
        }


        public async Task InitializeGame()
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.timer.Enabled = true;
            mapController.Map.BackgroundImageLocation = @"../../../Properties/Resources/background.png";
            await Clients.All.SendAsync("InitializeGame", mapController.Map.ToJson());
        }

        public void InitializePlayer(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();

            if (player == PlayerType.PLAYER1)
            {
                var player1 = mapController.Map.GetPlayer(player);

                player1.Coins = 0;
                player1.Tank = new Tank(new BaseGun(), new Point(30, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png", 100);
            }

            if (player == PlayerType.PLAYER2)
            {
                var player2 = mapController.Map.GetPlayer(player);

                player2.Coins = 0;
                player2.Tank = new Tank(new BaseGun(), new Point(900, 26), 5, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/tank.png", 100);
            }
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
