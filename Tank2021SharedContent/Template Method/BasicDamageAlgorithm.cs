using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Template_Method
{
    public class BasicDamageAlgorithm : DamageAlgorithmTemplate
    {
        protected override bool CanRandomCrit()
        {
            return false; // Cant crit
        }
    }
}
