using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent.Strategy
{
    public class FastMovement : MoveAlgorithm
    {
        public override void MoveDown(Tank tank)
        {
            var currentSpeed = tank.Speed;
            if (tank.Coordinates.Y + currentSpeed + ClientSideConstants.TankHeight <= ClientSideConstants.ClientHeight)
            {
                tank.Coordinates.Y += currentSpeed;
                tank.Rotation = RotateFlipType.RotateNoneFlipNone;
            }
        }

        public override void MoveLeft(Tank tank)
        {
            var currentSpeed = tank.Speed;
            if (tank.Coordinates.X - currentSpeed >= 0)
            {
                tank.Coordinates.X -= currentSpeed;
                tank.Rotation = RotateFlipType.Rotate270FlipX;
            }
        }

        public override void MoveRight(Tank tank)
        {
            var currentSpeed = tank.Speed;
            if (tank.Coordinates.X + currentSpeed + ClientSideConstants.TankHeight <= ClientSideConstants.ClientWidth)
            {
                tank.Coordinates.X += currentSpeed;
                tank.Rotation = RotateFlipType.Rotate90FlipX;
            }
        }

        public override void MoveUp(Tank tank)
        {
            var currentSpeed = tank.Speed;
            if (tank.Coordinates.Y - currentSpeed >= 0)
            {
                tank.Coordinates.Y -= currentSpeed;
                tank.Rotation = RotateFlipType.RotateNoneFlipY;
            }
        }
    }
}
