using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Observer.Subjects;

namespace Tank2021SharedContent.Facade
{
    public class BulletUtils
    {   
        public bool IsHittingTank(Tank tank, Bullet bullet)
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

        public void MoveBullet(Bullet bullet)
        {
            bullet.Move();
        }

        public bool CheckBulletForDeletion(Bullet bullet)
        {
            return bullet.MarkForDelete;
        }
        public void RemoveBulletFromGun(Gun gun, Bullet bullet)
        {
            gun.Bullets.Remove(bullet);
        }

    }
}
