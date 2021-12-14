using System;
using System.Drawing;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent
{
    public static class Helper
    {
        public static Size GetTankSize(Tank tank)
        {
            switch (tank.Rotation)
            {
                case RotateFlipType.RotateNoneFlipY:
                case RotateFlipType.RotateNoneFlipNone:
                    return new Size(ClientSideConstants.TankWidth, ClientSideConstants.TankHeight);
                case RotateFlipType.Rotate90FlipX:
                case RotateFlipType.Rotate270FlipX:
                    return new Size(ClientSideConstants.TankHeight, ClientSideConstants.TankWidth);

                default:
                    throw new ArgumentException($"Could not generate bullet location, unknown Rotation type - {tank.Rotation}");
            }
        }

        public static PlayerType GetOppositePlayer(PlayerType player)
        {
            switch (player)
            {
                case PlayerType.PLAYER1:
                    return PlayerType.PLAYER2;
                case PlayerType.PLAYER2:
                    return PlayerType.PLAYER1;
                default:
                    throw new ArgumentException($"No such player type - {player}");
            }
        }

        public static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return Direction.Up;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Right;
                default:
                    throw new ArgumentException($"No such direction - {direction}");
            }
        }

        public static int GetSpecificTankHp(Tank tank)
        {
            if (tank is LightTank)
                return ServerSideConstants.LightTankHealth;
            else if (tank is MediumTank)
                return ServerSideConstants.MediumTankHealth;
            else
                return ServerSideConstants.HeavyTankHealth;
        }

        public static Size GetBulletSize(Bullet bullet)
        {
            switch (bullet.Rotation)
            {
                case RotateFlipType.RotateNoneFlipY:
                case RotateFlipType.RotateNoneFlipNone:
                    return new Size(ClientSideConstants.BulletWidth, ClientSideConstants.BulletHeight);
                case RotateFlipType.Rotate90FlipX:
                case RotateFlipType.Rotate270FlipX:
                    return new Size(ClientSideConstants.BulletHeight, ClientSideConstants.BulletWidth);

                default:
                    throw new ArgumentException($"Could not generate bullet location, unknown Rotation type - {bullet.Rotation}");
            }
        }

        public static Point GetShotLocation(Tank tank)
        {
            switch (tank.Rotation)
            {
                case RotateFlipType.RotateNoneFlipY:
                case RotateFlipType.RotateNoneFlipNone:
                    return new Point(tank.Coordinates.X + (int)(ClientSideConstants.TankWidth / 2.7), tank.Coordinates.Y + (int)(ClientSideConstants.TankHeight / 2.7));
                case RotateFlipType.Rotate90FlipX:
                case RotateFlipType.Rotate270FlipX:
                    return new Point(tank.Coordinates.X + (int)(ClientSideConstants.TankHeight / 2.7), tank.Coordinates.Y + (int)(ClientSideConstants.TankWidth / 2.7));

                default:
                    throw new ArgumentException($"Could not generate bullet location, unknown Rotation type - {tank.Rotation}");
            }
        }
    }
}
