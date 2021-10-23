using System;
using System.Collections.Generic;
using System.Drawing;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Factory
{
    public class TankCreator : Creator
    {
        public override Tank CreateTank(TankType tankType)
        {
            var random = new Random();
            var randomLocation = new Point(random.Next(0, ClientSideConstants.ClientWidth - ClientSideConstants.TankWidth), random.Next(0, ClientSideConstants.ClientHeight - ClientSideConstants.TankHeight));

            switch (tankType)
            {
                case TankType.LightTank:
                    var lightTank = new LightTank(randomLocation, 8, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/light_tank.png", 
                        ServerSideConstants.LightTankHealth, new FastMovement());
                    var lightFactory = lightTank.GetAbstractFactory();

                    lightTank.Gun = lightFactory.CreateGun();
                    lightTank.Armor = lightFactory.CreateArmor();

                    return lightTank;

                case TankType.MediumTank:
                    var mediumTank = new MediumTank(randomLocation, 6, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/medium_tank.png", 
                        ServerSideConstants.MediumTankHealth, new FastMovement());
                    var mediumFactory = mediumTank.GetAbstractFactory();

                    mediumTank.Gun = mediumFactory.CreateGun();
                    mediumTank.Armor = mediumFactory.CreateArmor();

                    return mediumTank;

                case TankType.HeavyTank:
                    var heavyTank = new HeavyTank(randomLocation, 4, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/heavy_tank.png", 
                        ServerSideConstants.HeavyTankHealth, new FastMovement());
                    var heavyFactory = heavyTank.GetAbstractFactory();

                    heavyTank.Gun = heavyFactory.CreateGun();
                    heavyTank.Armor = heavyFactory.CreateArmor();

                    return heavyTank;

                default:
                    throw new ArgumentException($"Unknown tank type -> {tankType}");
            }
        }
    }
}
