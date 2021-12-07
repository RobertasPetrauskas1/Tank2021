using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021SharedContent.State
{
    public interface TankState
    {
        public void HandleChange();
        public void TransitionState();

    }
}
