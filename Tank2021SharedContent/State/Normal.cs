using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Proxy;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.State
{
    public class Normal : TankState
    {
        private Tank tank;
        public Normal(Tank tank)
        {
            this.tank = tank;
        }

        public void HandleChange()
        {
            // Do nothing
        }

        public void TransitionState()
        {
            tank.SetTankState(new SmokingProxy(tank));
        }
    }
}
