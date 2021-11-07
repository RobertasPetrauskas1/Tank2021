using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Command
{
    public static class CommandInvokerSingleton
    {
        static Invoker player1Invoker;
        static Invoker player2Invoker;
        public static void SetPlayer1Invoker(Invoker invoker)
        {
            player1Invoker = invoker;
        }
        public static void SetPlayer2Invoker(Invoker invoker)
        {
            player2Invoker = invoker;
        }
        public static Invoker GetPlayer1Invoker()
        {
            return player1Invoker;
        }
        public static Invoker GetPlayer2Invoker()
        {
            return player2Invoker;
        }
    }
}
