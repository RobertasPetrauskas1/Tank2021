using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Template_Method
{
    public class NormalCritAlgorithm : DamageAlgorithmTemplate
    {
        protected override bool CanRandomCrit()
        {
            return true;
        }
    }
}
