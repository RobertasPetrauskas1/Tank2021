using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.State
{
    public class Smoking : TankState
    {
        private Tank tank;
        private int QuarterHealth;
        public Smoking(Tank tank)
        {
            this.tank = tank;
            QuarterHealth = (int)(Helper.GetSpecificTankHp(tank) * 0.25);
        }
        public override void HandleChange()
        {
            tank.SetSmoking();
            tank.SetMoveAlgorithm(new MediumMovement());
        }

        public override void TryTransitionState()
        {
            if(tank.Health <= QuarterHealth)
            {
                tank.SetTankState(new Burning(tank));
            }
        }
    }
}
