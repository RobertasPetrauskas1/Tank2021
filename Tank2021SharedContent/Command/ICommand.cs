using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Command
{
    public interface ICommand
    {
        public void execute();
        public void undo();
    }
}
