using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Guns
{
    public class BaseGun : Gun
    {
        public BaseGun()
        {
            Cooldown = TimeSpan.FromSeconds(1); //1 second cooldown
            Damage = 10;
            Speed = 5;
        }
    }
}
