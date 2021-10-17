using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent
{
    public abstract class Gun
    {
        public int Damage;
        public int Speed;
        public Bullet Shoot(Point currentCoordinates, RotateFlipType flipType)
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
            return new Bullet(Damage, direction, currentCoordinates, Speed, flipType);
        }
    }
}
