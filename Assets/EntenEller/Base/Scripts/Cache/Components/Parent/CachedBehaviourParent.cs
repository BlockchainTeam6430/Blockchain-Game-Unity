using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Self;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Parent
{
    public abstract class CachedBehaviourParent : CachedBehaviourSelf
    {
        private CachedComponentParent parent;
        
        public T GetParent<T>() where T : Component
        {
            return (T) GetParent(typeof(T));
        }
        
        public object GetParent(Type type)
        {
            if (parent.IsNull()) parent = CreateCachedComponent<CachedComponentParent>();
            return parent.Get(type);
        } 
        
        public T[] GetAllParent<T>() where T : Component
        {
            return Array.ConvertAll(GetAllParent(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllParent(Type type)
        {
            if (parent.IsNull()) parent = CreateCachedComponent<CachedComponentParent>();
            return parent.GetAll(type);
        }
    }
}
