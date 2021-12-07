using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank2021Client.ChanOfResponsibility
{
    public class DefaultHandler : IKeyEventHandler
    {
        public Task HandleAsync(KeyEventArgs e)
        {
            return Task.CompletedTask;
            // LOG invalid key pressed
        }

        public void SetNextHandler(IKeyEventHandler nextHandler)
        {
            // Do nothing
        }
    }
}
