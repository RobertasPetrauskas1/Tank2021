using Microsoft.AspNetCore.SignalR;
using System;
using System.Drawing;
using System.Threading.Tasks;
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

                if (await IsGameOver(player1Tank, player2Tank))
                {
                    timer.Enabled = false;
                    ResetGame();
                }
                else
                {
                    UpdateBulletMovement(player1Gun, player2Tank);
                    UpdateBulletMovement(player2Gun, player1Tank);
                    await hubContext.Clients.All.SendAsync("UpdateMap", Map.ToJson());
                }
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

        private void ResetGame()
        {
            Map = new Map();
        }

        private bool IsTankDead(Tank tank)
        {
            if (tank.Health <= 0)
                return true;

            return false;
        }

        private void UpdateBulletMovement(Gun gun, Tank tank)
        {
            if (gun != null)
                for (var index = gun.Bullets.Count - 1; index > -1; index--)
                {
                    if (IsHittingTank(tank, gun.Bullets[index]))
                    {
                        tank.GetHit(gun.Bullets[index].Damage);
                        gun.Bullets.RemoveAt(index);
                    }
                    else if (gun.Bullets[index].MarkForDelete)
                    {
                        gun.Bullets.RemoveAt(index);
                    }
                    else
                    {
                        gun.Bullets[index].Move();
                    }
                }
        }

        private bool IsHittingTank(Tank tank, Bullet bullet)
        {
            if (tank != null)
            {
                var bulletRectangle = new Rectangle(bullet.Coordinates,
                    Helper.GetBulletSize(bullet));

                var tankRectangle = new Rectangle(tank.Coordinates,
                    Helper.GetTankSize(tank));

                if (tankRectangle.IntersectsWith(bulletRectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
