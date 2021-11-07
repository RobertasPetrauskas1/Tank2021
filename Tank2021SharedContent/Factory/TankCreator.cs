using System;
using System.Collections.Generic;
using System.Drawing;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Builders;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Decorator;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Factory
{
    public class TankCreator : Creator
    {
        public override Tank CreateTank(TankType tankType)
        {
            var random = new Random();
            Builder builder;
            var randomLocation = new Point(random.Next(0, ClientSideConstants.ClientWidth - ClientSideConstants.TankWidth), random.Next(0, ClientSideConstants.ClientHeight - ClientSideConstants.TankHeight));

            switch (tankType)
            {
                case TankType.LightTank:
                    var lightTank = new LightTank(randomLocation, RotateFlipType.RotateNoneFlipNone, new FastMovement());

                    builder = new LightTankBuilder(lightTank);
                    BuildTank(builder);
                    lightTank = (LightTank)GetTank(builder);

                    var lightFactory = lightTank.GetAbstractFactory();

                    lightTank.Gun = lightFactory.CreateGun();
                    lightTank.Armor = lightFactory.CreateArmor();

                    return lightTank;

                case TankType.MediumTank:
                    var mediumTank = new MediumTank(randomLocation, RotateFlipType.RotateNoneFlipNone,  new FastMovement());
                    var mediumFactory = mediumTank.GetAbstractFactory();

                    builder = new MediumTankBuilder(mediumTank);
                    BuildTank(builder);
                    mediumTank = (MediumTank)GetTank(builder);

                    mediumTank.Gun = mediumFactory.CreateGun();
                    mediumTank.Armor = mediumFactory.CreateArmor();

                    return mediumTank;

                case TankType.HeavyTank:
                    var heavyTank = new HeavyTank(randomLocation, RotateFlipType.RotateNoneFlipNone, new FastMovement());
                    var heavyFactory = heavyTank.GetAbstractFactory();

                    builder = new HeavyTankBuilder(heavyTank);
                    BuildTank(builder);
                    heavyTank = (HeavyTank)GetTank(builder);

                    heavyTank.Gun = heavyFactory.CreateGun();
                    heavyTank.Armor = heavyFactory.CreateArmor();

                    return heavyTank;

                case TankType.HeavyForcefieldTank:
                    var heavyForcefieldTank = new HeavyForcefieldTank(randomLocation, RotateFlipType.RotateNoneFlipNone, new FastMovement());
                    var heavyForcefieldFactory = heavyForcefieldTank.GetAbstractFactory();

                    builder = new HeavyForcefieldTankBuilder(heavyForcefieldTank);
                    BuildTank(builder);
                    heavyForcefieldTank = (HeavyForcefieldTank)GetTank(builder);

                    heavyForcefieldTank.Gun = heavyForcefieldFactory.CreateGun();
                    heavyForcefieldTank.Armor = heavyForcefieldFactory.CreateArmor();

                    heavyForcefieldTank.TankImage = new ForceField(heavyForcefieldTank.TankImage);

                    return heavyForcefieldTank;

                default:
                    throw new ArgumentException($"Unknown tank type -> {tankType}");
            }
        }

        public Tank GetTank(Builder builder)
        {
            return builder.GetResult();
        }

        public void BuildTank(Builder builder)
        {
            builder.BuildTankHealth();
            builder.BuildTankSpeed();
            builder.BuildTankPhoto();
        }
    }
}
