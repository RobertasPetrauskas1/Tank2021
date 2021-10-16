using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent
{
    public static class MapSingleton
    {
        static Map map;
        public static void setMapController(Map map)
        {
            MapSingleton.map = map;
        }
        public static Map getMapController()
        {
            return map;
        }
    }
}
