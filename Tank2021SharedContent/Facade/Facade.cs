using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Observer.Subjects;

namespace Tank2021SharedContent.Facade
{
    public class Facade
    {
        TankConfigurer tankConfigurer;
        BulletUtils bulletUtils;
        GameStatus gameStatus;

        public Facade()
        {
            tankConfigurer = new TankConfigurer();
            bulletUtils = new BulletUtils();
        }

        public void ConfigureTank(Tank player1Tank, Tank player2Tank)
        {
            if (player1Tank != null)
            {
                tankConfigurer.ConfigureMoveAlgorithm(player1Tank);
                tankConfigurer.ConfigureTankImage(player1Tank);
            }
            if (player2Tank != null)
            {
                tankConfigurer.ConfigureMoveAlgorithm(player2Tank);
                tankConfigurer.ConfigureTankImage(player2Tank);
            }
        }

        public void UpdateBulletMovement(Gun gun, Tank tank)
        {
            if (gun != null)
                for (var index = gun.Bullets.Count - 1; index > -1; index--)
                {
                    Bullet bullet = gun.Bullets[index];
                    if (bulletUtils.IsHittingTank(tank, bullet))
                    {
                        tank.GetHit(bullet.Damage);
                        bulletUtils.RemoveBulletFromGun(gun, bullet);
                        UpdateGameStatus();
                    }
                    else if (bulletUtils.CheckBulletForDeletion(bullet))
                    {
                        bulletUtils.RemoveBulletFromGun(gun, bullet);
                    }
                    else
                    {
                        bulletUtils.MoveBullet(gun.Bullets[index]);
                    }
                }
        }

        public void SetGameStatus(GameStatus status)
        {
            gameStatus = status;
        }

        private void UpdateGameStatus()
        {
            var map = MapSingleton.getMap();
            gameStatus.UpdateStatus(map);
        }
    }
}
