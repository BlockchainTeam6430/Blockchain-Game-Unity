using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RandomScale : EEBehaviour
    {
        [SerializeField] private float min, max;
        
        public void Call()
        {
            var r = Random.Range(min, max);
            GetSelf<Transform>().localScale = new Vector3(r, r, r);
        }
    }
}
