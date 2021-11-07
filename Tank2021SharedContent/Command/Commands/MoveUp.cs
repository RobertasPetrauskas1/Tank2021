using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.Command.Commands
{
    public class MoveUp : ICommand
    {
        Map map;
        PlayerType player;
        public MoveUp(Map map, PlayerType playerType)
        {
            this.map = map;
            player = playerType;
        }
        public void execute()
        {
            map.MoveUp(player);
        }

        public void undo()
        {
            map.MoveDown(player);
        }
    }
}
