using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Token.Caretaker
{
    public static class CaretakerSingleton
    {
        private static Caretaker _caretaker;
        public static void setCaretaker(Caretaker caretaker)
        {
            _caretaker = caretaker;
        }
        public static Caretaker getCaretaker()
        {
            return _caretaker;
        }
    }
}
