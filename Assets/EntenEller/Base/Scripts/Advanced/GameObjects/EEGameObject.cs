using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.GameObjects
{
    [ExecutionOrder(-9999)]
    [DisallowMultipleComponent] 
    public class EEGameObject : EEGameObjectTransform
    {
        [ReadOnly] public bool IsActive = false;
        [ReadOnly] public EEGameObject Spawner;

        protected override void EEAwake()
        {
            base.EEAwake();
            IsActive = true;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            IsActive = true;
            EventOn();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            IsActive = false;
            EventOff();
        }

        public void On()
        {
            if (IsActive) return;
            gameObject.SetActive(true);
        }
        
        public void Off()
        {
            if (!IsActive) return;
            gameObject.SetActive(false);
        }

        public void Spawn()
        {
            EEGameObjectUtils.Add(this);
            foreach (var ts in GetAllChild<Transform>())
            {
                EEGameObjectUtils.Add(ts.GetEEGameObject());
            }
        }

        public void SetState(bool isOn)
        {
            if (isOn) On();
            else Off();
        }
        
        public void DisableAndRemoveFromParent()
        {
            Off();
            UnsetParent();
        }

        public void Destroy()
        {
            EEGameObjectUtils.Remove(this);
            UnsetParent();
            Destroy(gameObject);
        }
        
        public void SetSpawner(EEGameObject spawner)
        {
            Spawner = spawner;
        }
    }
}