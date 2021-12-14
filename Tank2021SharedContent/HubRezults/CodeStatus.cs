using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.HubRezults
{
    public class CodeStatus
    {
        public bool Success { get; set; }
        public PlayerType PlayerType { get; set; }

        public CodeStatus(bool success, PlayerType playerType)
        {
            Success = success;
            PlayerType = playerType;
        }
    }
}
