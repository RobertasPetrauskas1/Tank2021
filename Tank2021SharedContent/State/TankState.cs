using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021SharedContent.State
{
    public abstract class TankState
    {
        public abstract void HandleChange();
        public abstract void TryTransitionState();

    }
}
