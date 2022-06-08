using System;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Spawns
{
    public abstract class EESpawnZoneBase : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetAllChild<EESpawner>().ForEach(a => a.BeforeEnable += BeforeEnable);
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetAllChild<EESpawner>().ForEach(a => a.BeforeEnable -= BeforeEnable);
            GetAllChild<EESpawner>().ForEach(a => a.DespawnAll());
        }

        private void BeforeEnable(EEGameObject obj)
        {
            obj.transform.position = GetSpawnPosition();
        }

        public void Spawn()
        {
            var i = 0;
            var spawners = GetAllChild<EESpawner>();
            
            while (i < 100000)
            {
                var spawner = spawners.GetRandom();
                if (spawner.GetSelf<CollectionFloat>().List.First() > Random.value)
                {
                    spawner.Spawn();
                    return;
                }
                i++;
            }
            throw new Exception("Cannot spawn! " + gameObject.name);
        }

        public abstract Vector3 GetSpawnPosition();
    }
}
