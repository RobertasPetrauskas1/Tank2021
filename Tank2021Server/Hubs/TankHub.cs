using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Threading.Tasks;
using Tank2021Server;
using Tank2021Server.Observer.Observers;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Factory;
using Tank2021SharedContent.Observer.Subjects;

namespace Tank2021.Hubs
{
    public class TankHub : Hub
    {
        public async Task ConnectPlayer(PlayerType player, TankType tank)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.GetPlayer(player).IsConnected = true;
            InitializePlayer(player, tank);
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
            mapController.InitGameStatus();
            mapController.InitGameoverObservable();
            mapController.timer.Enabled = true;
            mapController.Map.BackgroundImageLocation = @"../../../Properties/Resources/background.png";
            await Clients.All.SendAsync("InitializeGame", mapController.Map.ToJson());
        }

        public void InitializePlayer(PlayerType player, TankType tank)
        {
            var mapController = MapControllerSingleton.getMapController();
            var creator = new TankCreator();

            if (player == PlayerType.PLAYER1)
            {
                var player1 = mapController.Map.GetPlayer(player);
                player1.Tank = creator.CreateTank(tank);
            }

            if (player == PlayerType.PLAYER2)
            {
                var player2 = mapController.Map.GetPlayer(player);
                player2.Tank = creator.CreateTank(tank);
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
