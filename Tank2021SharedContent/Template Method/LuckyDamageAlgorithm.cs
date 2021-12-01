using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Template_Method
{
    public class LuckyDamageAlgorithm : DamageAlgorithmTemplate
    {
        protected override bool CanRandomCrit()
        {
            return true;
        }

        protected override bool RollForCrit()
        {
            Random random = new Random();
            var n = random.NextDouble();
            return n >= 0.50; // 50% chance of being a crit
        }
    }
}
