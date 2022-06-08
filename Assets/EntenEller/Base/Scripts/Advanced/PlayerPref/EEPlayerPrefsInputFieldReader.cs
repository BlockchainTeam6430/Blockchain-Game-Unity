using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using TMPro;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.PlayerPref
{
    [RequireComponent(typeof(EEPlayerPrefs))]
    public class EEPlayerPrefsInputFieldReader : EEBehaviour
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            Read();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEPlayerPrefs>().UpdateAllSuperAction.Event += Read;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEPlayerPrefs>().UpdateAllSuperAction.Event -= Read;
        }

        public void Read()
        {
            GetSelf<TMP_InputField>().text = GetSelf<EEPlayerPrefs>().Data;
        }
    }
}
