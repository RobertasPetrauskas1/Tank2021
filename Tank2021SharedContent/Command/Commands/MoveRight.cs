using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.Command.Commands
{
    public class MoveRight : ICommand
    {
        Map map;
        PlayerType player;
        public MoveRight(Map map, PlayerType playerType)
        {
            this.map = map;
            player = playerType;
        }
        public void execute()
        {
            map.MoveRight(player);
        }

        public void undo()
        {
            map.MoveLeft(player);
        }
    }
}
