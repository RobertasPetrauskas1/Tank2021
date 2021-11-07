using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Adapter;

namespace Tank2021SharedContent.AbstractFactories
{
    class HeavyForcefieldFactory : AbstractFactory
    {
        public override Armor CreateArmor()
        {
            return new ForceFieldArmorAdapter(new ForceField(7));
        }

        public override Gun CreateGun()
        {
            return new HeavyGun();
        }
    }
}
