using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Abstract.Armors
{
    public abstract class Armor : IUnit
    {
        public abstract int DamageReduction { get; set; }
        public abstract int HitsLeft { get; set; }

        public Armor Copy()
        {
            return (Armor)this.MemberwiseClone();
        }

        public virtual int GetHitsLeft()
        {
            return HitsLeft;
        }

        public virtual int GetDamageReduction()
        {
            return DamageReduction;
        }
        public virtual void GetHit()
        {
            if(HitsLeft > 0)
            {
                HitsLeft--;
            }
        }
    }
}
