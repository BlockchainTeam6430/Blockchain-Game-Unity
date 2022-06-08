using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Patterns.Pool
{
    public class EEPoolObject : EEGameObject
    {
        public EEPoolObject Origin;
        public int J;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            if (GetSelf<Rigidbody>()) GetSelf<Rigidbody>().velocity = Vector3.zero;
        }
    }
}