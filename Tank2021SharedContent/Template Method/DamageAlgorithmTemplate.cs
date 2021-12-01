using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Guns;

namespace Tank2021SharedContent.Template_Method
{
    public abstract class DamageAlgorithmTemplate
    {
        public int CalculateDamage(int baseDamage)
        {
            int damage = baseDamage;
            if (CanRandomCrit())
            {
                if (RollForCrit())
                {
                    damage = ModifyDamage(damage);
                }
            }
            return damage;
        }

        protected abstract bool CanRandomCrit();

        protected virtual bool RollForCrit()
        {
            Random random = new Random();
            var n = random.NextDouble();
            return n >= 0.80; // 20% chance of being a crit
        }

        protected virtual int ModifyDamage(int currentDamage)
        {
            double CRIT_MODIFIER = 1.25;
            return (int)Math.Round(currentDamage * CRIT_MODIFIER, MidpointRounding.AwayFromZero);
        }

    }
}
