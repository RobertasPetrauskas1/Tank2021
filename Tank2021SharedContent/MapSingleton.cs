using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent
{
    public static class MapSingleton
    {
        static Map map;
        public static void setMap(Map map)
        {
            MapSingleton.map = map;
        }
        public static Map getMap()
        {
            return map;
        }
    }
}
