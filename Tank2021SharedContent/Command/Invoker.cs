using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Command
{
    public class Invoker
    {
        Stack<ICommand> history;

        public Invoker()
        {
            history = new Stack<ICommand>();
        }

        public void execute(ICommand command)
        {
            command.execute();
            history.Push(command);
        }

        public void undoLast()
        {
            if(history.Count != 0)
            {
                ICommand previousCommand = history.Pop();
                previousCommand.undo();
            }
        }
    }
}
