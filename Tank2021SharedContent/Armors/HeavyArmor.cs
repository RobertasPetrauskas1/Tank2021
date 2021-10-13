using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract;

namespace Tank2021SharedContent.Armors
{
    public class HeavyArmor : Armor
    {
        public HeavyArmor()
        {
            DamageReduction = 40;
            HitsLeft = 150;
        }
    }
}
