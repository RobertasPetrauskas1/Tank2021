using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent.Builders
{
    public class LightTankBuilder : Builder
    {
        public Tank Tank { get; set; }
        public LightTankBuilder(Tank tank)
        {
            Tank = tank;
        }
        public override void BuildTankHealth()
        {
            Tank.SetHealth(ServerSideConstants.LightTankHealth);
        }

        public override void BuildTankPhoto()
        {
            Tank.SetPhoto(@"../../../Properties/Resources/light_tank.png");
        }

        public override void BuildTankSpeed()
        {
            Tank.SetSpeed(8);
        }

        public override Tank GetResult()
        {
            return Tank;
        }
    }
}
