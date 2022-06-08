using System;
using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.Patterns.Pool;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool
{
    public class EENetPoolObject : EEPoolObject
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!EEServerGlobalEvents.IsActive) return;
            foreach (var client in EEServerGlobalEvents.Clients)
            {
                Sync(client);
            }
            EEServerGlobalEvents.ClientConnectedEvent += Sync;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            if (!EEServerGlobalEvents.IsActive) return;
            EEServerGlobalEvents.ClientConnectedEvent -= Sync;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            if (!EEServerGlobalEvents.IsActive) return;
            foreach (var client in EEServerGlobalEvents.Clients)
            {
                Sync(client);
            }
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            if (!EEServerGlobalEvents.IsActive) return;
            foreach (var client in EEServerGlobalEvents.Clients)
            {
                Sync(client);
            }
        }

        private void Sync(EEPeer peer)
        {
            var (i, j) = EESingleton.Get<EENetPoolManager>().GetIJ(this);
            var data = new EENetPoolObjectData
            {
                I = i,
                J = j,
                State = IsActive,
                Position = EEByteSerializer.Serialize(transform.position)
            };
            EEPacketToClient.Send(peer, typeof(EENetPoolManager), data);
        }

        [Serializable]
        public class EENetPoolObjectData
        {
            public int I;
            public int J;
            public bool State;
            public byte[] Position;
        }
    }
}
