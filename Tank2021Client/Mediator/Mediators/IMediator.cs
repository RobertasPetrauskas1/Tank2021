using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021Client.Mediator.Colleagues;
using Tank2021Client.Mediator.Enum;

namespace Tank2021Client.Mediator.Mediators
{
    public interface IMediator
    {
        void Notify(ColleagueType sender, object data, string ev);
        void AddColleague(BaseColleague colleague);
    }
}
