using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnZone3D : EESpawnZoneBase
    {
        public override Vector3 GetSpawnPosition()
        {
            var bounds = GetSelf<BoxCollider>().bounds;
            return VectorUtils.RandomV3(bounds.center - bounds.size / 2, bounds.center + bounds.size / 2);
        }
    }
}
