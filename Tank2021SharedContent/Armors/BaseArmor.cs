using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract;

namespace Tank2021SharedContent.Armors
{
    public class BaseArmor : Armor
    {
       public BaseArmor()
        {
            DamageReduction = 0;
            HitsLeft = int.MaxValue;
        }

        public override void GetHit()
        {
            //Cant destroy base armor
            HitsLeft -= 0;
        }
    }
}
