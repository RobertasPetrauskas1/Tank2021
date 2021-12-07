using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Proxy;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.State
{
    public class Burning : TankState
    {
        private Tank tank;
        public Burning(Tank tank)
        {
            this.tank = tank;
        }
        public void HandleChange()
        {
            tank.SetOnFire();
            tank.SetMoveAlgorithm(new SlowMovement());
        }

        public void TransitionState()
        {
            tank.SetTankState(new CriticalProxy(tank));
        }
    }
}
