using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank2021SharedContent.Interpreter
{
    public class CheatCommand : IEnumerable<string>
    {
        public List<string> commands;
        private const char Seperator = ' ';

        public CheatCommand(string text)
        {
            commands = text.Split(Seperator).ToList();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
