using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Spawns
{
    public abstract class EESpawner : EEReadyBehaviour
    {
        [SerializeField] private EEGameObjectFinder parentToStick;
        [SerializeField] protected EEGameObject Prefab;
        [SerializeField] private bool isGetOnlyParentTransformValues;
        [SerializeField] [ReadOnly] protected List<EEGameObject> Spawned = new List<EEGameObject>();
        public Action<EEGameObject> BeforeEnable;
        [SerializeField] private bool isResetRPS = true;
        
        public EEGameObject Spawn()
        {
            var obj = OnSpawn();
            obj.SetParent(parentToStick.GetSingle(this), isResetRPS);
            Spawned.Add(obj);
            BeforeEnable.Call(obj);
            if (isGetOnlyParentTransformValues) obj.UnsetParent();
            obj.SetSpawner(this.GetEEGameObject());
            obj.On();
            return obj;
        }
        
        public void SimpleSpawn()
        {
            Spawn();
        }
        
        public void DespawnLast()
        {
            var obj = Spawned.Last();
            OnDespawn(obj);
            Spawned.Remove(obj);
        }
        
        public void DespawnAll()
        {
            while (Spawned.Count > 0)
            {
                DespawnLast();
            }
        }
        
        protected abstract EEGameObject OnSpawn();
        protected abstract void OnDespawn(EEGameObject obj);
    }
}
