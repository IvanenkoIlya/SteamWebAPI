using SteamGames.SteamWebAPI.Attributes;
using System;

namespace SteamGames.SteamWebAPI.ISteamUser
{
    public static class SteamUser
    {
        private static SteamManager instance = SteamManager.GetInstance();

        private static string base_url = "https://api.steampowered.com/ISteamUser/";
        private static string publisher_base_url = "https://partner.steam-api.com/ISteamUser/";

        [RequiresPublisher]
        public static void CheckAppOwnership( string steamid, int appid)
        {
            //TODO this needs to return custom object, cannot create custom object without publisher API key
            throw new NotImplementedException();

            //string request_url = publisher_base_url + string.Format("CheckAppOwnership/v1?steamid={0}&appid={1}", steamid, appid);

            //instance.GetResponse<object>(request_url); 
        }

        [RequiresPublisher]
        public static void GetAppPriceInfo(long steamid, string appid)
        {
            //TODO this needs to return custom object, cannot create custom object without publisher API key

            throw new NotImplementedException();

            //string request_url = publisher_base_url + string.Format("GetAppPriceInfo/v1?steamid={0}&appid={1}", steamid, appid);

            //instance.GetResponse<object>(request_url);
        }

        public static void GetFriendsList(long steamid, string relationship = "")
        {
            string request_url = base_url + string.Format("GetFriendList/v1?steamid={0}", steamid);

            instance.GetResponse<object>(request_url);
        }

        public static UserId ResolveVanityURL(string username, int type = 1)
        {
            string request_url = base_url + string.Format("ResolveVanityURL/v1?vanityurl={0}&url_type={1}", username, type);

            return instance.GetResponse<Response<UserId>>(request_url).response;
        }
    }
}