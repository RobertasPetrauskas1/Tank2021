using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Abstract.Armors
{
    public class HeavyArmor : Armor
    {
        public override int DamageReduction { get; set; }
        public override int HitsLeft { get; set; }

        public HeavyArmor()
        {
            DamageReduction = 5;
            HitsLeft = 10;
        }
    }
}
