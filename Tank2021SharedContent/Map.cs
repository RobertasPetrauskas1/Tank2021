using Newtonsoft.Json;
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

        public void MoveDown(PlayerType player) => GetPlayer(player).Tank.Move(Direction.Down);
        public void MoveUp(PlayerType player) => GetPlayer(player).Tank.Move(Direction.Up);
        public void MoveLeft(PlayerType player) => GetPlayer(player).Tank.Move(Direction.Left);
        public void MoveRight(PlayerType player) => GetPlayer(player).Tank.Move(Direction.Right);
        public void Shoot(PlayerType player) => GetPlayer(player).Tank.Shoot();
        public string ToJson() => JsonConvert.SerializeObject(CopyShallow(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });

        public Map CopyShallow()
        {
            return (Map)this.MemberwiseClone();
        }

        public Map CopyDeep()
        {
            var map = (Map)this.MemberwiseClone();
            map.player1 = map.player1.Copy();
            map.player2 = map.player2.Copy();
            if(map.player1.Tank != null)
                CopyPlayer(map.player1);
            if (map.player2.Tank != null)
                CopyPlayer(map.player2);

            return map;
        }

        public void CopyPlayer(Player player)
        {
            player.Tank = player.Tank.Copy();
            player.Tank.Armor = player.Tank.Armor.Copy();
            player.Tank.Gun = player.Tank.Gun.Copy();

            for(var index = 0; index < player.Tank.Gun.Bullets.Count; index++)
            {
                player.Tank.Gun.Bullets[index] = player.Tank.Gun.Bullets[index].Copy();
            }
        }
    }
}
