using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Patterns.Pool;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnerEEPoolObject : EESpawner
    {
        protected override EEGameObject OnSpawn()
        {
            var obj = EEPool.GetAvailablePoolObject(Prefab as EEPoolObject);
            return obj;
        }

        protected override void OnDespawn(EEGameObject obj)
        {
            obj.Off();
        }
    }
}