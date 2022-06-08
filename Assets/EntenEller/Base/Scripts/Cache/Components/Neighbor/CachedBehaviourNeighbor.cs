using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Child;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Neighbor
{
    public abstract class CachedBehaviourNeighbor : CachedBehaviourChild
    {
        private CachedComponentNeighbor neighbor;
        
        public T GetNeighbor<T>() where T : Component
        {
            return (T) GetNeighbor(typeof(T));
        }
        
        public object GetNeighbor(Type type)
        {
            if (neighbor.IsNull()) neighbor = CreateCachedComponent<CachedComponentNeighbor>();
            return neighbor.Get(type);
        }
        
        public T[] GetAllNeighbor<T>() where T : Component
        {
            return Array.ConvertAll(GetAllNeighbor(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllNeighbor(Type type)
        {
            if (neighbor.IsNull()) neighbor = CreateCachedComponent<CachedComponentNeighbor>();
            return neighbor.GetAll(type);
        }
    }
}
