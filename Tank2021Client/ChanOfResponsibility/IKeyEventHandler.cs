using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.ChanOfResponsibility
{
    public interface IKeyEventHandler
    {
        public void SetNextHandler(IKeyEventHandler nextHandler);
        public Task HandleAsync(KeyEventArgs e);
    }
}
