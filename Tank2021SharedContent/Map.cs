using Newtonsoft.Json.Linq;
using System;

namespace Tank2021SharedContent
{
    public class Map
    {
        public Player player1;
        public Player player2;

        public Map()
        {
            player1 = new Player();
            player2 = new Player();
        }

        public Player GetPlayer(PlayerType type)
        {
            return type == PlayerType.PLAYER1 ? player1 : player2;
        }
        public string ToJson()
        {
            JObject mapJson = (JObject)JToken.FromObject(this);
            return mapJson.ToString();
        }
    }
}
