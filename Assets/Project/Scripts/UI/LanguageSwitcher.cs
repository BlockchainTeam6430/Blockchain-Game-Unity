using System;
using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class LanguageSwitcher : EEBehaviour
    {
        [SerializeField] List<string> variants;
        private int index;

        public void Next()
        {
            index++;
            Get();
        }

        public void Previous()
        {
            index--;
            Get();
        }

        private void Get()
        {
            Plugins.EntenEller.Base.Scripts.Translation.LanguageManager.Instance.Switch(variants.IndexOf(variants.GetLooped(index)));
        }
    }
}
