using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Abstract
{
    public abstract class Armor
    {
        public int DamageReduction;
        public int HitsLeft;

        public virtual void GetHit()
        {
            HitsLeft -= 1;
            if(HitsLeft <= 0)
            {
                //TODO: break current armor, give BaseArmor
            }
        }
    }
}
