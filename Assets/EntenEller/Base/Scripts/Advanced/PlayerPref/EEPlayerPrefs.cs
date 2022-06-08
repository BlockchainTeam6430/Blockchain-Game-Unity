using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.PlayerPref
{
    public class EEPlayerPrefs : EEBehaviour
    {
        public string Key;
        [ReadOnly] public string Data;
        public EESuperAction UpdateAllSuperAction;
        
        public static event Action WriteEvent;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            Read();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            WriteEvent += Read;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            WriteEvent -= Read;
        }

        private void Read()
        {
            Data = PlayerPrefs.GetString(Key, string.Empty);
            UpdateAllSuperAction.Call(ComponentID);
        }
        
        public void Write(string data)
        {
            PlayerPrefs.SetString(Key, data);
            WriteEvent.Call();
        }
    }
}
