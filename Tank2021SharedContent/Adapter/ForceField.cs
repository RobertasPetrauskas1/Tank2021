using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Adapter
{
    public class ForceField
    {
        public int DamageNegation { get; set; }

        public ForceField()
        {

        }
        public ForceField(int damageNegation)
        {
            DamageNegation = damageNegation;
        }

        public int GetDamageNegation()
        {
            return DamageNegation;
        }
    }
}
