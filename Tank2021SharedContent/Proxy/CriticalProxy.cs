using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.State;

namespace Tank2021SharedContent.Proxy
{
    public class CriticalProxy : TankState
    {
        private Tank tank;
        private Critical CriticalState;
        public CriticalProxy(Tank tank)
        {
            this.tank = tank;
            CriticalState = new Critical(tank);
        }

        public void HandleChange()
        {
            CriticalState.HandleChange();
        }

        public void TransitionState()
        {
            CriticalState.TransitionState();
        }
    }
}
