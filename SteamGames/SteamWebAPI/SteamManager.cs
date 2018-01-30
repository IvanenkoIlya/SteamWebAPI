using Newtonsoft.Json;
using SteamGames.SteamWebAPI.ISteamUser;
using System.IO;
using System.Net;

namespace SteamGames.SteamWebAPI
{
    class SteamManager
    {
        private static string key;
        private static SteamManager instance;

        public static UserId user;

        private SteamManager() { }

        public static SteamManager GetInstance()
        {
            if (key == null)
                throw new System.Exception("SetKey must be called first to create an instance");

            if (instance == null)
                instance = new SteamManager();
            return instance;
        }

        public static SteamManager SetKey(string steam_key)
        {
            key = steam_key;
            return GetInstance();
        }

        public T GetResponse<T>(string url)
        {
            url += "&key=" + key;

            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            return GetResponseContent<T>(response);
        }

        private T GetResponseContent<T>(WebResponse response)
        {
            T responseObj;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {

                responseObj = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }

            return responseObj;
        }

        public void UseSteamUser(string user_name)
        {
            user = SteamUser.ResolveVanityURL(user_name);
        }

        public long GetUserId()
        {
            return user.SteamId;
        }
    }

    public class Response<T>
    {
        public T response { get; set; }
    }
}
