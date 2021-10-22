using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Abstract.Armors
{
    public class MediumArmor : Armor 
    {
        public override int DamageReduction { get; set; }
        public override int HitsLeft { get; set; }

        public MediumArmor()
        {
            DamageReduction = 3;
            HitsLeft = 7;
        }
    }
}
