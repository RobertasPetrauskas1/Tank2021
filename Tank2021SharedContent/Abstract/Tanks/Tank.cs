﻿using System;
using System.Drawing;
using Tank2021SharedContent.Abstract;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.AbstractFactories;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Decorator;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Proxy;
using Tank2021SharedContent.State;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Abstract.Tanks
{
    public abstract class Tank
    {
        public abstract Gun Gun { get; set; }
        public abstract Armor Armor { get; set; }
        public abstract int Health { get; set; }
        public abstract int Speed { get; set; }
        public abstract UnitImage TankImage { get; set; }

        public TankState TankState { get; set; }

        public RotateFlipType Rotation { get; set; }
        public MoveAlgorithm MoveAlgorithm { get; set; }
        public Point Coordinates;

        public Tank(Point coordinates, RotateFlipType rotation, MoveAlgorithm moveAlgorithm)
        {
            Coordinates = coordinates;
            Rotation = rotation;
            MoveAlgorithm = moveAlgorithm;
            TankState = new NormalProxy(this);
        }

        public abstract AbstractFactory GetAbstractFactory();

        public void SetTankState(TankState state)
        {
            TankState = state;
            TankState.HandleChange();
        }

        public void SetSmoking()
        {
            TankImage = new Smoke(TankImage);
        }

        public void SetOnFire()
        {
            TankImage = new Fire(TankImage);
        }

        public void SetCriticalyDamaged()
        {
            TankImage = new Danger(TankImage);
        }

        public void SetMoveAlgorithm(MoveAlgorithm moveAlgorithm) => MoveAlgorithm = moveAlgorithm;

        public void SetHealth(int health) => Health = health;

        public void SetSpeed(int speed) => Speed = speed;

        public void SetPhoto(string imageFileName) => TankImage = new TankImage(imageFileName);

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

        public Tank Copy()
        {
            return (Tank)this.MemberwiseClone();
        }

        public void GetHit(int damage)
        {
            var currentDamage = damage;
            if(Armor.GetHitsLeft() > 0)
            {
                currentDamage = Armor.GetDamageReduction() >= damage ? 0 : damage - Armor.GetDamageReduction();
                Armor.GetHit();
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

        public void Shoot()
        {
            Gun.Shoot(Helper.GetShotLocation(this), Rotation);
        }
    }
}
