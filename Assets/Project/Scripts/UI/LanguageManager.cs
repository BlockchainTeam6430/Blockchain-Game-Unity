using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class LanguageManager : Plugins.EntenEller.Base.Scripts.Translation.LanguageManager
    {
        [SerializeField] private List<string> keys = new List<string>();
        [SerializeField] private List<string> jsons = new List<string>();
        
        public void Load()
        {
            var list = new List<Dictionary<string, string>>();
            for (var i = 0; i < keys.Count; i++)
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsons[i]);
                list.Add(dict);
            }

            foreach (var kv in list.First())
            {
                var key = kv.Key;
                var texts = new List<string>();
                foreach (var dict in list)
                {
                    texts.Add(dict[key]);
                }
                AddData(key, texts);
            }

            Switch(0);
        }
    }
}
