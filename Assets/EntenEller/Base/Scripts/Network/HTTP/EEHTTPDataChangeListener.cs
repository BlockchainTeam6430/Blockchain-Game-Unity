using EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPDataChangeListener : EEBehaviour
    {
        public string DataCurrent;
        public EESuperAction ChangedSuperAction;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEHTTPStringReceiver>().SuccessEvent += data =>
            {
                if (DataCurrent != string.Empty && DataCurrent != data)
                {
                    print(DataCurrent != data);
                    ChangedSuperAction.Call(ComponentID);
                }
                DataCurrent = data;
            };
        }
    }
}
