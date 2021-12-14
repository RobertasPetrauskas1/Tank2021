using System;
using System.Collections.Generic;
using static Tank2021SharedContent.Interpreter.Context;

namespace Tank2021SharedContent.Token.Caretaker
{
    public class Caretaker
    {
        public IList<Momento> momentos;

        public Caretaker()
        {
            momentos = new List<Momento>();
        }

        public void AddMomento(Momento momento) => momentos.Add(momento);
        public int GetLastIndex() => momentos.Count - 1;

        public Momento Undo(int index)
        {
            if (index > -1 && index < momentos.Count)
                return momentos[index];

            return null;
        }

        public void Remove(int index)
        {
            if (index > -1 && index < momentos.Count)
                momentos.RemoveAt(index);
        }
    }
}
