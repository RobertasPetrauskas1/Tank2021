using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract;

namespace Tank2021SharedContent.Armors
{
    public class LightArmor : Armor
    {
        public LightArmor()
        {
            DamageReduction = 10;
            HitsLeft = 100;
        }
    }
}
