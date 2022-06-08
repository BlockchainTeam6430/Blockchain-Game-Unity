using System;
using BestHTTP;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPByteReceiver : EEBehaviour
    {
        public Action<byte[]> SuccessEvent;
   
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
            SuccessEvent.Call(result.Data);
        }
    }
}
