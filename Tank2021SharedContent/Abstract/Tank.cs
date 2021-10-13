using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract;

namespace Tank2021SharedContent
{
    public abstract class Tank
    {
        public int Hitpoints { get; set; }
        public Gun Gun { get; set; }
        public Armor Armor { get; set; }

        public void GetHit(int damage)
        {
            int calculatedDamage = damage - Armor.DamageReduction;
            calculatedDamage = calculatedDamage < 0 ? 0 : calculatedDamage;
            
            Hitpoints = Hitpoints - calculatedDamage;
            if(Hitpoints <= 0)
            {
                //TODO: Gameover
            }

            Armor.GetHit();
        }
    }
}
