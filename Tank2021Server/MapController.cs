using Microsoft.AspNetCore.SignalR;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Timers;
using Tank2021.Hubs;
using Tank2021Server.Observer.Observers;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Decorator;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Facade;
using Tank2021SharedContent.Observer.Observers;
using Tank2021SharedContent.Observer.Subjects;
using Tank2021SharedContent.Strategy;

namespace Tank2021Server
{
    public class MapController
    {
        public Map Map { get; set; }
        public GameStatus gameStatus;
        public IHubContext<TankHub> hubContext;
        public Timer timer = new Timer();
        public static double timerSpeed = 30;
        public IObserver gameOverObserver;
        public Facade facade;

        public MapController(IHubContext<TankHub> hubContext)
        {
            //Map = new Map();
            MapSingleton.setMap(new Map());
            Map = MapSingleton.getMap();
            facade = FacadeSingleton.getFacade();
            this.hubContext = hubContext;
            timer.Interval = timerSpeed;
            timer.Enabled = false;
            timer.Start();

            AddGameCycle();
        }

        private void AddGameCycle()
        {
            timer.Elapsed += async (Object source, ElapsedEventArgs e) =>
            {
                var mapController = MapControllerSingleton.getMapController();

                var player1Tank = mapController.Map.GetPlayer(PlayerType.PLAYER1)?.Tank;
                var player2Tank = mapController.Map.GetPlayer(PlayerType.PLAYER2)?.Tank;

                var player1Gun = player1Tank?.Gun;
                var player2Gun = player2Tank?.Gun;

                facade.UpdateBulletMovement(player1Gun, player2Tank);
                facade.UpdateBulletMovement(player2Gun, player1Tank);
                facade.ConfigureTank(player1Tank, player2Tank);
                await hubContext.Clients.All.SendAsync("UpdateMap", Map.ToJson());
            };
        }


        private async Task<bool> IsGameOver(Tank player1Tank, Tank player2Tank)
        {
            if(player1Tank == null || player2Tank == null)
            {
                return false;
            }
            else if (IsTankDead(player1Tank))
            {
                await hubContext.Clients.All.SendAsync("GameOver", PlayerType.PLAYER1);
                return true;
            }
            else if (IsTankDead(player2Tank))
            {
                await hubContext.Clients.All.SendAsync("GameOver", PlayerType.PLAYER2);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetGame()
        {
            timer.Enabled = false;
            //Map = new Map();
            MapSingleton.setMap(new Map());
            Map = MapSingleton.getMap();
        }

        private bool IsTankDead(Tank tank)
        {
            return tank.Health <= 0;
        }

        public void InitGameStatus()
        {
            var player1Tank = Map.GetPlayer(PlayerType.PLAYER1).Tank;
            var player2Tank = Map.GetPlayer(PlayerType.PLAYER2).Tank;
            gameStatus = new GameStatus(new PlayerInfo(player1Tank.Health, player1Tank.MoveAlgorithm), new PlayerInfo(player2Tank.Health, player2Tank.MoveAlgorithm));
            facade.SetGameStatus(gameStatus);
        }

        public void InitGameoverObservable()
        {
            gameOverObserver = new GameOverObserver(gameStatus, hubContext);
        }
    }
}
