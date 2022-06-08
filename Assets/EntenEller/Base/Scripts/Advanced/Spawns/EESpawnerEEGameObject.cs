using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnerEEGameObject : EESpawner
    {
        protected override EEGameObject OnSpawn()
        {
            return SpawnUtils.Spawn(Prefab);
        }

        protected override void OnDespawn(EEGameObject obj)
        {
            obj.Destroy();
        }
    }
}
