using System;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Template_Method;

namespace Tank2021SharedContent.Abstract.Guns
{
    public class LightGun : Gun
    {
        public override TimeSpan Cooldown { get; set; }
        public override int Damage { get; set; }
        public override int Speed { get; set; }

        public LightGun()
        {
            Cooldown = TimeSpan.FromSeconds(0.25); //15 second cooldown
            Damage = 5;
            Speed = 10;
            DamageAlgorithm = new BasicDamageAlgorithm();
        }
    }
}
