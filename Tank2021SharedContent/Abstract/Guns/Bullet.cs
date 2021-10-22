using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent.Abstract.Guns
{
    public class Bullet
    {
        public int Damage { get; set; }
        public Direction FlightDirection { get; set; }
        public int Speed { get; set; }
        public RotateFlipType Rotation { get; set; }
        public string ImageLocation { get; set; }

        public Point Coordinates;
        public bool MarkForDelete = false;

        public Bullet(int damage, Direction direction, Point coordinates, int speed, RotateFlipType rotation)
        {
            Damage = damage;
            FlightDirection = direction;
            Coordinates = coordinates;
            Speed = speed;
            Rotation = rotation;
            ImageLocation = @"../../../Properties/Resources/small_bullet.png";
        }

        public void Move()
        {
            switch (FlightDirection) 
            {
                case Direction.Up:
                    MoveUp();
                    break;
                case Direction.Right:
                    MoveRight();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
                case Direction.Left:
                    MoveLeft();
                    break;
            }
        }

        private void MoveDown()
        {
            if (Coordinates.Y + Speed + ClientSideConstants.BulletHeight <= ClientSideConstants.ClientHeight)
            {
                Coordinates.Y += Speed;
                Rotation = RotateFlipType.RotateNoneFlipNone;
            }
            else
            {
                MarkForDelete = true;
            }
        }

        private void MoveUp()
        {
            if (Coordinates.Y - Speed >= 0)
            {
                Coordinates.Y -= Speed;
                Rotation = RotateFlipType.RotateNoneFlipY;
            }
            else
            {
                MarkForDelete = true;
            }
        }

        private void MoveRight()
        {
            if (Coordinates.X + Speed + ClientSideConstants.BulletHeight <= ClientSideConstants.ClientWidth)
            {
                Coordinates.X += Speed;
                Rotation = RotateFlipType.Rotate90FlipX;
            }
            else
            {
                MarkForDelete = true;
            }
        }

        private void MoveLeft()
        {
            if (Coordinates.X - Speed >= 0)
            {
                Coordinates.X -= Speed;
                Rotation = RotateFlipType.Rotate270FlipX;
            }
            else
            {
                MarkForDelete = true;
            }
        }
    }
}
