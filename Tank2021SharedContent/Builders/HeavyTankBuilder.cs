using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent.Builders
{
    public class HeavyTankBuilder : Builder
    {
        public Tank Tank { get; set; }
        public HeavyTankBuilder(Tank tank)
        {
            Tank = tank;
        }

        public override void BuildTankHealth()
        {
            Tank.SetHealth(ServerSideConstants.HeavyTankHealth);
        }

        public override void BuildTankPhoto()
        {
            Tank.SetPhoto(@"heavy_tank.png");
        }

        public override void BuildTankSpeed()
        {
            Tank.SetSpeed(4);
        }

        public override Tank GetResult()
        {
            return Tank;
        }
    }
}
