using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public class EEUnityEventServerActive : EEBehaviour
    {
        [SerializeField] private EESuperAction OnServer;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (EEServerGlobalEvents.IsActive)
            {
                OnServer.Call();
            }
            else
            {
                EEServerGlobalEvents.ConnectedEvent += Connect;

                void Connect()
                {
                    EEClientGlobalEvents.ConnectedEvent -= Connect;
                    OnServer.Call();
                }
            }
        }
    }
}