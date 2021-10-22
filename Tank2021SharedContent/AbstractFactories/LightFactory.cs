using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;

namespace Tank2021SharedContent.AbstractFactories
{
    public class LightFactory : AbstractFactory
    {
        public override Armor CreateArmor()
        {
            return new LightArmor();
        }

        public override Gun CreateGun()
        {
            return new LightGun();
        }
    }
}
