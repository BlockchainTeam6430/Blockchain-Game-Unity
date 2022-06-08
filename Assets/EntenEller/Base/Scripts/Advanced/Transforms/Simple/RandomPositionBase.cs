using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public abstract class RandomPositionBase : EEBehaviour
    {
        [SerializeField] protected EEGameObjectFinder Target;
        
        public void Call()
        {
            GetSelf<Transform>().position = GetRandom();
        }

        public abstract (Vector3 min, Vector3 max) GetBounds();

        public Vector3 GetRandom()
        {
            var (min, max) = GetBounds();
            return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        }
    }
}
