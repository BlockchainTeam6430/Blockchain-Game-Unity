using System.Collections.Generic;
using BestHTTP;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPHeaders : EEBehaviour
    {
        [SerializeField] private List<StringUtils.KeyValueStrings> headers = new List<StringUtils.KeyValueStrings>();

        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEHTTP>().NewRequestEvent += NewRequest;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEHTTP>().NewRequestEvent -= NewRequest;
        }
        
        private void NewRequest(HTTPRequest request)
        {
            foreach (var kv in headers)
            {
                request.AddHeader(kv.Key, kv.Value);
            }
        }
    }
}
