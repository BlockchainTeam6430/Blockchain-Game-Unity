using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using TMPro;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.PlayerPref
{
    [RequireComponent(typeof(EEPlayerPrefs))]
    public class EEPlayerPrefsEETextWriter : EEBehaviour
    {
        public void Write()
        {
            var data = GetSelf<TextMeshProUGUI>()?.text;
            GetSelf<EEPlayerPrefs>().Write(data);
        }
    }
}
