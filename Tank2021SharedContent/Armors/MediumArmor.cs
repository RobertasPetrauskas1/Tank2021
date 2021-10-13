using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract;

namespace Tank2021SharedContent.Armors
{
    public class MediumArmor : Armor
    {
        public MediumArmor()
        {
            DamageReduction = 20;
            HitsLeft = 120;
        }
    }
}
