using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.Command.Commands
{
    public class MoveDown : ICommand
    {
        Map map;
        PlayerType player;
        public MoveDown(Map map, PlayerType playerType)
        {
            this.map = map;
            player = playerType;
        }

        public void execute()
        {
            map.MoveDown(player);
        }

        public void undo()
        {
            map.MoveUp(player);
        }
    }
}
