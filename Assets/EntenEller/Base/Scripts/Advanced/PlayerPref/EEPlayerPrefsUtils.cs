using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.PlayerPref
{
    public static class EEPlayerPrefsUtils 
    {
        public static void Write(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        
        public static string Read(string key)
        {
            return PlayerPrefs.GetString(key);
        }
    }
}
