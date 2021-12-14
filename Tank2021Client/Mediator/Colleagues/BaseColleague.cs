using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021Client.Mediator.Enum;
using Tank2021Client.Mediator.Mediators;

namespace Tank2021Client.Mediator.Colleagues
{
    public abstract class BaseColleague
    {
        protected IMediator _mediator;

        public BaseColleague(IMediator mediator = null)
        {
            _mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public abstract ColleagueType getType();
        public abstract void SendData(object data, string ev);
        public abstract void ReceiveData(object data, string ev);
    }
}
