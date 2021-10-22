using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;

namespace Tank2021SharedContent.AbstractFactories
{
    public abstract class AbstractFactory
    {
        public abstract Gun CreateGun();
        public abstract Armor CreateArmor();
    }
}
