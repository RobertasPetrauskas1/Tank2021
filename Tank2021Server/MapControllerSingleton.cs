namespace Tank2021Server
{
    public static class MapControllerSingleton
    {
        static MapController mapController;
        public static void setMap(MapController map)
        {
            MapControllerSingleton.mapController = map;
        }
        public static MapController getMapController()
        {
            return mapController;
        }
    }
}
