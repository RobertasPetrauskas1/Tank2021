using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Template_Method;

namespace Tank2021SharedContent.Abstract.Guns
{
    public class HeavyGun : Gun
    {
        public override TimeSpan Cooldown { get; set; }
        public override int Damage { get; set; }
        public override int Speed { get; set; }

        public HeavyGun()
        {
            Cooldown = TimeSpan.FromSeconds(1); //1 second cooldown
            Damage = 10;
            Speed = 5;
            DamageAlgorithm = new LuckyDamageAlgorithm();
        }
    }
}
