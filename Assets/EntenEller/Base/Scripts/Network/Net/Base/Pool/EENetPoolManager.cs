using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Patterns.Pool;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool
{
    public class EENetPoolManager : EEBehaviour, IEENetReceiverClient
    {
        [SerializeField] private List<EEPoolObject> pool;

        public (int, int) GetIJ(EEPoolObject poolObject)
        {
            var i = pool.IndexOf(poolObject.Origin);
            var j = EEPool.GetJByPoolObject(poolObject);
            return (i, j);
        }

        public EEPoolObject GetOrigin(int i)
        {
            return pool[i];
        }

        public void ReceiveAsClient(object data)
        {
            var poolObjectData = (EENetPoolObject.EENetPoolObjectData) data;
            var obj = EEPool.GetPoolObjectByJ(GetOrigin(poolObjectData.I),  poolObjectData.J);
            
            var v3 = EEByteSerializer.Deserialize<Vector3>(poolObjectData.Position);
            obj.GetSelf<Transform>().position = v3;
            
            if (obj.GetSelf<TransformApproachPositionSimple>()) obj.GetSelf<TransformApproachPositionSimple>().Position.Current = obj.GetSelf<Transform>().position;
            obj.SetState(poolObjectData.State);
        }
    }
}
