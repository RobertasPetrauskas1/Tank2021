using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Abstract.Armors
{
    public abstract class Armor : IUnit
    {
        public abstract int DamageReduction { get; set; }
        public abstract int HitsLeft { get; set; }
    }
}
