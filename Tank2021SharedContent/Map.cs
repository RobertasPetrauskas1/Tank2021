using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent
{
    public class Map
    {
        public Player player1;
        public Player player2;
        public string BackgroundImageLocation;

        public Map()
        {
            player1 = new Player();
            player2 = new Player();
        }

        public Player GetPlayer(PlayerType type) => type == PlayerType.PLAYER1 ? player1 : player2;
        public Player GetOppositePlayer(PlayerType type) => type == PlayerType.PLAYER1 ? player2 : player1;

        public void MoveDown(PlayerType player) => GetPlayer(player).Tank.MoveDown();
        public void MoveUp(PlayerType player) => GetPlayer(player).Tank.MoveUp();
        public void MoveLeft(PlayerType player) => GetPlayer(player).Tank.MoveLeft();
        public void MoveRight(PlayerType player) => GetPlayer(player).Tank.MoveRight();
        public void Shoot(PlayerType player) => GetPlayer(player).Tank.Shoot();
        public string ToJson() => JsonConvert.SerializeObject(this.MemberwiseClone(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
    }
}
