using Microsoft.Win32;
using SteamGames.SteamWebAPI;
using System;
using System.Collections.Generic;
using System.IO;

namespace SteamGames
{
    class Program
    {
        static void Main(string[] args)
        {
            string registry_key = @"Software\Valve\Steam";
            string subkey_name = "SteamPath";

            string steam_dir;
            List<string> steam_install = new List<string>();
            List<string> game_names = new List<string>();

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registry_key))
            {
                steam_dir = key.GetValue(subkey_name).ToString().Replace("/",@"\");
            }

            string steam_config = steam_dir + @"\config\config.vdf";

            //Console.WriteLine(steam_config);

            using (StreamReader sr = new StreamReader(File.OpenRead(steam_config)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if(line.Contains("BaseInstallFolder_")) 
                    {
                        steam_install.Add(line.Split('"')[3]);
                        //Console.WriteLine(line.Split('"')[3]);
                    }
                }
            }

            steam_install.Add(steam_dir);

            foreach (string dir in steam_install)
            {
                string temp = dir + @"\steamapps\common\";
                game_names = new List<string>( Directory.GetDirectories(temp));
            }

            foreach( string game in game_names)
            {
                string game_name = game.Split('\\')[game.Split('\\').Length-1];

                if ( Directory.GetFiles(game, "steam_appid.txt", SearchOption.AllDirectories).Length > 0)
                {
                    Console.WriteLine(game_name + "[X]");
                } else
                {
                    Console.WriteLine(game_name + "[ ]");
                }
            }

            SteamManager steamManager = SteamManager.SetKey("F048AF532EAEC0278D40732105FA0057");
            

            Console.ReadLine();
        }
    }
}
