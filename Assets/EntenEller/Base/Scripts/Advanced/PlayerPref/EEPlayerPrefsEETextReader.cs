using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.PlayerPref
{
    [RequireComponent(typeof(EEPlayerPrefs))]
    public class EEPlayerPrefsEETextReader : EEBehaviour
    {
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

        private void Read()
        {
            GetSelf<EETextSimple>().SetData(GetSelf<EEPlayerPrefs>().Data);
        }
    }
}
