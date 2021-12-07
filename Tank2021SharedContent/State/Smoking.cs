using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Proxy;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.State
{
    public class Smoking : TankState
    {
        private Tank tank;
        public Smoking(Tank tank)
        {
            this.tank = tank;
        }
        public void HandleChange()
        {
            tank.SetSmoking();
            tank.SetMoveAlgorithm(new MediumMovement());
        }

        public void TransitionState()
        {
            tank.SetTankState(new BurningProxy(tank));
        }
    }
}
