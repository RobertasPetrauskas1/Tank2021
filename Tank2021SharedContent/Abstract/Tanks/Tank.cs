using System;
using System.Drawing;
using Tank2021SharedContent.Abstract;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.AbstractFactories;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Abstract.Tanks
{
    public abstract class Tank
    {
        public abstract Gun Gun { get; set; }
        public abstract Armor Armor { get; set; }
        public abstract int Health { get; set; }
        public abstract int Speed { get; set; }
        public abstract string ImageLocation { get; set; }

        public RotateFlipType Rotation { get; set; }
        public MoveAlgorithm MoveAlgorithm { get; set; }
        public Point Coordinates;

        public Tank(Point coordinates, RotateFlipType rotation, MoveAlgorithm moveAlgorithm)
        {
            Coordinates = coordinates;
            Rotation = rotation;
            MoveAlgorithm = moveAlgorithm;
        }

        public abstract AbstractFactory GetAbstractFactory();

        public void SetMoveAlgorithm(MoveAlgorithm moveAlgorithm)
        {
            MoveAlgorithm = moveAlgorithm;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    MoveAlgorithm.MoveUp(this);
                    break;
                case Direction.Down:
                    MoveAlgorithm.MoveDown(this);
                    break;
                case Direction.Right:
                    MoveAlgorithm.MoveRight(this);
                    break;
                case Direction.Left:
                    MoveAlgorithm.MoveLeft(this);
                    break;
            }
        }

        public void GetHit(int damage)
        {
            var currentDamage = damage;
            if(Armor.HitsLeft > 0)
            {
                currentDamage = Armor.DamageReduction >= damage ? 0 : damage - Armor.DamageReduction;
                Armor.HitsLeft--;
            }

            Health -= currentDamage;
        }

        public bool IsHittingBorder(int width, int height)
        {
            if (Coordinates.X < 0 || Coordinates.X + width > ClientSideConstants.ClientWidth)
                return true;
            if (Coordinates.Y < 0 || Coordinates.Y + height > ClientSideConstants.ClientHeight)
                return true;

            return false;
        }

        //public void MoveDown()
        //{
        //    if(Coordinates.Y + Speed + ClientSideConstants.TankHeight <= ClientSideConstants.ClientHeight)
        //    {
        //        Coordinates.Y += Speed;
        //        Rotation = RotateFlipType.RotateNoneFlipNone;
        //    }
        //}

        //public void MoveUp()
        //{
        //    if(Coordinates.Y - Speed >= 0)
        //    {
        //        Coordinates.Y -= Speed;
        //        Rotation = RotateFlipType.RotateNoneFlipY;
        //    }
        //}

        //public void MoveRight()
        //{
        //    if(Coordinates.X + Speed + ClientSideConstants.TankHeight <= ClientSideConstants.ClientWidth)
        //    {
        //        Coordinates.X += Speed;
        //        Rotation = RotateFlipType.Rotate90FlipX;
        //    }
        //}

        //public void MoveLeft()
        //{
        //    if(Coordinates.X - Speed >= 0)
        //    {
        //        Coordinates.X -= Speed;
        //        Rotation = RotateFlipType.Rotate270FlipX;
        //    }
        //}

        public void Shoot()
        {
            Gun.Shoot(Helper.GetShotLocation(this), Rotation);
        }
    }
}
