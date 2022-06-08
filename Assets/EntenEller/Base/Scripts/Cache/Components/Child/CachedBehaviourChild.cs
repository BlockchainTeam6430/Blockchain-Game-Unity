using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Child
{
    public abstract class CachedBehaviourChild : CachedBehaviourMaster
    {
        private CachedComponentChild child;
        
        public T GetChild<T>() where T : Component
        {
            return (T) GetChild(typeof(T));
        }
        
        public object GetChild(Type type)
        {
            if (child.IsNull()) child = CreateCachedComponent<CachedComponentChild>();
            return child.Get(type);
        }
        
        public T[] GetAllChild<T>() where T : Component
        {
            return Array.ConvertAll(GetAllChild(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllChild(Type type)
        {
            if (child.IsNull()) child = CreateCachedComponent<CachedComponentChild>();
            return child.GetAll(type);
        }
    }
}