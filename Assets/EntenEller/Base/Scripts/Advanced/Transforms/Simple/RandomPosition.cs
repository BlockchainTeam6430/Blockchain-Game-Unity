using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RandomPosition : RandomPositionBase
    {
        [SerializeField] private Vector3 minPos, maxPos;
        
        public override (Vector3 min, Vector3 max) GetBounds()
        {
            return (minPos, maxPos);
        }
    }
}
