using System.Collections.Generic;
using EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Blockchain;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class Registration : EEBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        
        public void Call()
        {
            var dict = new Dictionary<string, string>
            {
                {"password", inputField.text}, 
                {"wallet", Wallet.Account}
            };
            GetSelf<EEHTTPStringSender>().SetNewData(dict);
            GetSelf<EEHTTP>().Call();
        }
    }
}
