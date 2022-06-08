using System;
using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Translation
{
    public class LanguageManager : EEBehaviour
    {
        private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
        public int Language;
        public Action<int> SwitchedEvent;
        public EESuperAction SwitchedSuperAction = new EESuperAction();
        public static LanguageManager Instance;

        protected override void EEAwake()
        {
            base.EEAwake();
            Instance = this;
        }

        public void Switch(int language)
        {
            Language = language;
            SwitchedEvent.Call(Language);
            SwitchedSuperAction.Call(ComponentID);
        }

        public void AddData(string key, List<string> value)
        {
            data.Add(key, value);
        }

        public string GetTranslation(string key)
        {
            var result = data.TryGetValue(key, out var list);
            if (!result) return key;
            return list.Count <= Language ? "NO LANGUAGE!" : list[Language];
        }
    }
}
