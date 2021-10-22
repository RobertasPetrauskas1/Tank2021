using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent.Abstract.Guns
{
    public abstract class Gun : IUnit
    {
        public abstract TimeSpan Cooldown { get; set; }
        public abstract int Damage { get; set; }
        public abstract int Speed { get; set; }

        public List<Bullet> Bullets = new List<Bullet>();
        public DateTime NextShootTime = DateTime.Now;

        public virtual void Shoot(Point currentCoordinates, RotateFlipType flipType)
        {
            if(DateTime.Now >= NextShootTime)
            {
                Direction direction;
                switch (flipType)
                {
                    case RotateFlipType.RotateNoneFlipY:
                        direction = Direction.Up;
                        break;
                    case RotateFlipType.RotateNoneFlipNone:
                        direction = Direction.Down;
                        break;
                    case RotateFlipType.Rotate90FlipX:
                        direction = Direction.Right;
                        break;
                    case RotateFlipType.Rotate270FlipX:
                        direction = Direction.Left;
                        break;
                    default:
                        throw new Exception("Unknown RotateFlipType");
                }

                Bullets.Add(new Bullet(Damage, direction, currentCoordinates, Speed, flipType));
                NextShootTime = DateTime.Now.Add(Cooldown);
            }
        }
    }
}
