using Plugins.EntenEller.Base.Scripts.Advanced.Transforms;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class EEGameObjectTransform : EEUnityEventBinary
    {
        public EESuperAction ChangedHierarchySuperAction = new EESuperAction();
        private int frameOfChanging;

        protected override void EEEnable()
        {
            base.EEEnable();
            ChangedHierarchySuperAction.Call(ComponentID);
        }

        public void SetParent(EEGameObject newParent, bool isResetRPS = true)
        {
            var oldParent = transform.parent;
            if (isResetRPS)
            {
                transform.SetParentResetPRS(newParent.transform);
            }
            else
            {
                transform.SetParentSavePRS(newParent.transform);
            }
            CleanCachedComponents(GetSelf<Transform>());
            if (oldParent) CleanCachedComponents(oldParent);
        }
        
        public void SetParentResetRPS(EEGameObject newParent)
        {
            SetParent(newParent);
        }
        
        public void SetParentSaveRPS(EEGameObject newParent)
        {
            SetParent(newParent, false);
        }
        
        public void UnsetParent()
        {
            Transform oldParent = null;
            if (transform.parent)
            {
                oldParent = transform.parent;
            }
            GetSelf<Transform>().SetParentSavePRS(null);
            CleanCachedComponents(GetSelf<Transform>());
            if (oldParent) CleanCachedComponents(oldParent);
        }

        protected void CleanCachedComponents(Transform target)
        {
            if (frameOfChanging == Time.frameCount) return;
            var parent = target.GetFirstParent();
            var listObj = parent.GetComponentsInChildren<EEGameObject>(true);
            for (var i = 0; i < listObj.Length; i++)
            {
                var obj = listObj[i];
                obj.frameOfChanging = Time.frameCount;
                obj.ChangedHierarchySuperAction.Call(obj.ComponentID);
            }
            var children = parent.GetComponentsInChildren<CachedComponent>(true);
            for (var i = 0; i < children.Length; i++)
            {
                children[i].Clean();
            }
        }
    }
}
