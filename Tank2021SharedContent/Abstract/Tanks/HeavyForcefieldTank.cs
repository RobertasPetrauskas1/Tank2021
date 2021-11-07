using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.AbstractFactories;
using Tank2021SharedContent.Decorator;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Abstract.Tanks
{
    class HeavyForcefieldTank : Tank
    {
        public override Gun Gun { get; set; }
        public override Armor Armor { get; set; }
        public override int Health { get; set; }
        public override int Speed { get; set; }
        public override UnitImage TankImage { get; set; }

        public HeavyForcefieldTank(Point coordinates, RotateFlipType rotation, MoveAlgorithm moveAlgorithm) :
            base(coordinates, rotation, moveAlgorithm)
        {
        }

        public override AbstractFactory GetAbstractFactory()
        {
            return new HeavyForcefieldFactory();
        }
    }
}
