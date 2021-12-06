using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.State
{
    public class Critical : TankState
    {
        private Tank tank;
        public Critical(Tank tank)
        {
            this.tank = tank;
        }
        public override void HandleChange()
        {
            tank.SetCriticalyDamaged();
            tank.SetMoveAlgorithm(new StopMovement());
        }

        public override void TryTransitionState()
        {
            // No further state
        }
    }
}
