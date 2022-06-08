using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public class EEUnityEventClientStateChanged : EEUnityEventBinary
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            if (EEClientGlobalEvents.IsConnected)
            {
                EventOn();
            }
            else
            {
                EEClientGlobalEvents.ConnectedEvent += Connect;
                EEClientGlobalEvents.ClosedEvent += Closed;
                
                void Connect()
                {
                    EEClientGlobalEvents.ConnectedEvent -= Connect;
                    EventOn();
                }

                void Closed()
                {
                    EEClientGlobalEvents.ClosedEvent -= Closed;
                    EventOff();
                }
            }
        }
    }
}