using System;
using System.ComponentModel;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using Component = UnityEngine.Component;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Self
{
    [DisallowMultipleComponent]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class CachedComponentSelf : CachedComponent
    {
        public object Get(Type type)
        {
            var result = ComponentsSingle.TryGetValue(type, out var component);
            if (result) return component;
            component = EEComponentUtils.EETryGetComponent(gameObject, type);
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            ComponentsSingle.Add(type, component);
            return component;
        }
        
        public Component[] GetAll(Type type)
        {
            var result = ComponentsAll.TryGetValue(type, out var components);
            if (result) return components;
            components = gameObject.GetComponents(type);
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            ComponentsAll.Add(type, components);
            return components;
        }
    }
}
