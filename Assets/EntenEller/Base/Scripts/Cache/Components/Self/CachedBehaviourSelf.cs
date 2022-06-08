using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Neighbor;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Self
{
    public abstract class CachedBehaviourSelf : CachedBehaviourNeighbor
    {
        private CachedComponentSelf self;
        
        public T GetSelf<T>() where T : Component
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return GetComponent<T>();
#endif
            return (T) GetSelf(typeof(T));
        }
        
        public object GetSelf(Type type)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return GetComponent(type);
#endif
            if (self.IsNull()) self = CreateCachedComponent<CachedComponentSelf>();
            return self.Get(type);
        } 
        
        public T[] GetAllSelf<T>() where T : Component
        {
            return Array.ConvertAll(GetAllSelf(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllSelf(Type type)
        {
            if (self.IsNull()) self = CreateCachedComponent<CachedComponentSelf>();
            return self.GetAll(type);
        }
    }
}
