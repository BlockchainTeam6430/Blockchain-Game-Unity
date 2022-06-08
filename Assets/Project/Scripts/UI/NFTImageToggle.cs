using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.UI.Toggles;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class NFTImageToggle : EEBehaviourLoop
    {
        public static Texture2D CurrentTexture;
        public static string URL;
        public static string CurrentID = string.Empty;
        public string ID;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEToggle>().ValueIsOnSuperAction.Event += IsOn;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEToggle>().ValueIsOnSuperAction.Event -= IsOn;
        }

        private void IsOn()
        {
            CurrentTexture = GetChild<EEHTTPTexture>().Texture;
            URL = GetChild<EEHTTPTexture>().GetSelf<EEHTTP>().url;
            CurrentID = ID;
            PlayerPrefs.SetString("avatar", CurrentID);
            PlayerPrefs.Save();
        }

        protected override void Loop()
        {
            base.Loop();
            if (CurrentID == string.Empty)
            {
                var r = PlayerPrefs.GetString("avatar", string.Empty);
                if (r != string.Empty)
                {
                    if (ID == r) CurrentID = ID;
                }
                else if (transform.GetSiblingIndex() == 0)
                {
                    CurrentID = ID;
                }
            }
            if (ID == CurrentID)
            {
                if (!GetSelf<EEToggle>().IsOn)
                {
                    GetSelf<EEToggle>().SetOn();
                }
                CurrentTexture = GetChild<EEHTTPTexture>().Texture;
            }
        }
    }
}
