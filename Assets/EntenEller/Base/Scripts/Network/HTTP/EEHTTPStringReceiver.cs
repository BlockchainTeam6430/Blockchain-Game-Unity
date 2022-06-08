using System;
using BestHTTP;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using UnityEngine;

namespace EntenEller.Base.Scripts.Network.HTTP
{
    [ExecuteBefore(typeof(EEHTTP))]
    public class EEHTTPStringReceiver : EEBehaviour
    {
        [SerializeField] [TextArea(1, 50)] private string stringResult;
        public Action<string> SuccessEvent;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEHTTP>().SuccessEvent += Success;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEHTTP>().SuccessEvent -= Success;
        }
        
        private void Success(HTTPResponse result)
        {
            stringResult = result.DataAsText;
            SuccessEvent.Call(stringResult);
        }
    }
}
