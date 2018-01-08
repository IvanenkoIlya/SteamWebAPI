namespace SteamGames.SteamWebAPI
{
    class SteamManager
    {
        private static string key;
        private static SteamManager instance;

        private SteamManager() { }

        private static SteamManager GetInstance()
        {
            return instance ?? (instance = new SteamManager());
        }

        public static SteamManager SetKey( string steam_key)
        {
            key = steam_key;
            return GetInstance();
        }
    }
}
