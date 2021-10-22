using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Abstract.Guns
{
    public class MediumGun : Gun
    {
        public override TimeSpan Cooldown { get; set; }
        public override int Damage { get; set; }
        public override int Speed { get; set; }

        public MediumGun()
        {
            Cooldown = TimeSpan.FromSeconds(0.5); //30 second cooldown
            Damage = 7;
            Speed = 7;
        }
    }
}
