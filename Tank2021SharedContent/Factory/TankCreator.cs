using System;
using System.Collections.Generic;
using System.Drawing;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Enums;

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
                    var lightTank = new LightTank(randomLocation, 8, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/light_tank.png", 100);
                    var lightFactory = lightTank.GetAbstractFactory();

                    lightTank.Gun = lightFactory.CreateGun();
                    lightTank.Armor = lightFactory.CreateArmor();

                    return lightTank;

                case TankType.MediumTank:
                    var mediumTank = new MediumTank(randomLocation, 6, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/medium_tank.png", 125);
                    var mediumFactory = mediumTank.GetAbstractFactory();

                    mediumTank.Gun = mediumFactory.CreateGun();
                    mediumTank.Armor = mediumFactory.CreateArmor();

                    return mediumTank;

                case TankType.HeavyTank:
                    var heavyTank = new HeavyTank(randomLocation, 4, RotateFlipType.RotateNoneFlipNone, @"../../../Properties/Resources/heavy_tank.png", 150);
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
