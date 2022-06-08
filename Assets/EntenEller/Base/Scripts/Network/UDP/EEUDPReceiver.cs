using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.UDP
{
    public class EEUDPReceiver : EEBehaviourLoop
    {
        [Serializable]
        public class UDPData
        {
            public bool IsActive = true;
            public byte[] RawData;
        }
        
        private bool isActive;
        [SerializeField] [ReadOnly] private List<UDPData> cache = new List<UDPData>();
       
        public Action<byte[]> DataReceivedEvent;
        public EESuperAction ReceivedSuperAction;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEUDP>().OnSuperAction.Event += StartListen;
            GetSelf<EEUDP>().OffSuperAction.Event += StopListen;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEUDP>().OnSuperAction.Event -= StartListen;
            GetSelf<EEUDP>().OffSuperAction.Event -= StopListen;
        }
        
        private async void StartListen()
        {
            isActive = true;
            var client = GetSelf<EEUDP>().Client;
            var endPoint = GetSelf<EEUDP>().EndPoint;
            
            await Listen();
            
            async Task Listen()
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (!isActive) return;
                        cache.RemoveAll(a => a.IsActive == false); 
                        cache.Add(new UDPData {RawData = client.Receive(ref endPoint)});
                    }
                });
            }
        }
        
        private void StopListen()
        {
            isActive = false;
        }
        
        protected override void Loop()
        {
            base.Loop();
            var i = 0;
            while (true)
            {
                if (i >= cache.Count) return;
                var udpData = cache[i];
                i++;
                if (udpData == null) continue;
                if (!udpData.IsActive) continue;
                ReceivedSuperAction.Call(ComponentID);
                udpData.IsActive = false;
                DataReceivedEvent.Call(udpData.RawData);
            }
        }
    }
}