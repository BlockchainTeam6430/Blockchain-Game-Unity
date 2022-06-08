using System.Collections.Generic;
using BestHTTP;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using UnityEngine;

namespace EntenEller.Base.Scripts.Network.HTTP
{
    [ExecuteBefore(typeof(EEHTTP))]
    public class EEHTTPStringSender : EEBehaviour
    {
        [SerializeField] private List<StringUtils.KeyValueStrings> data;

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
            foreach (var kv in data)
            {
                request.AddField(kv.Key, kv.Value);
            }
        }

        public void SetNewData(Dictionary<string, string> dictionary)
        {
            data.Clear();
            foreach (var kv in dictionary)
            {
                var k = new StringUtils.KeyValueStrings
                {
                    Key = kv.Key,
                    Value = kv.Value
                };
                data.Add(k);
            }
        }
    }
}
