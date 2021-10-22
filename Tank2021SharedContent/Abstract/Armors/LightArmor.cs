using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Abstract.Armors
{
    public class LightArmor : Armor
    {
        public override int DamageReduction { get; set; }
        public override int HitsLeft { get; set; }

        public LightArmor()
        {
            DamageReduction = 1;
            HitsLeft = 5;
        }
    }
}
