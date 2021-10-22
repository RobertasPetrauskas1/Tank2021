using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;

namespace Tank2021SharedContent.AbstractFactories
{
    public class MediumFactory : AbstractFactory
    {
        public override Armor CreateArmor()
        {
            return new MediumArmor();
        }

        public override Gun CreateGun()
        {
            return new MediumGun();
        }
    }
}
