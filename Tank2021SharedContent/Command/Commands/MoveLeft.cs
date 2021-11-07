﻿using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.Command.Commands
{
    public class MoveLeft : ICommand
    {
        Map map;
        PlayerType player;
        public MoveLeft(Map map, PlayerType playerType)
        {
            this.map = map;
            player = playerType;
        }
        public void execute()
        {
            map.MoveLeft(player);
        }

        public void undo()
        {
            map.MoveRight(player);
        }
    }
}
