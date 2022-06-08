using Plugins.EntenEller.Base.Scripts.Blockchain;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class IDText : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<TextMeshProUGUI>().SetText(Wallet.Account);
        }
    }
}
