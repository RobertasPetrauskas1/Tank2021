using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Armors;

namespace Tank2021SharedContent.Adapter
{
    public class ForceFieldArmorAdapter : Armor
    {
        public override int DamageReduction { get; set; }
        public override int HitsLeft { get; set; }

        public ForceField forceField;

        public ForceFieldArmorAdapter()
        {

        }

        public ForceFieldArmorAdapter(ForceField forceField)
        {
            this.forceField = forceField;
        }

        public override int GetDamageReduction()
        {
            return forceField.GetDamageNegation();
        }

        public override void GetHit()
        {
            //Do nothing. Forcefields can't be destroyed
        }

        public override int GetHitsLeft()
        {
            // Force fields cant be deleted. Return max int value.
            return Int32.MaxValue;
        }
    }
}
