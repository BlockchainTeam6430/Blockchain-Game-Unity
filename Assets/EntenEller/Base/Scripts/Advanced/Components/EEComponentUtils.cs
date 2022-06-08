using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Components
{
    public static class EEComponentUtils
    {
        public static List<T> FindAll<T>() where T : Component
        {
            return EEGameObjectUtils.EEGameObjects.Select(eeGameObject => eeGameObject.GetSelf<T>()).Where(menu => menu).ToList();
        }

        public static bool IsNull(this Object obj)
        {
            return obj == null;
        }
        
        public static bool IsNotNull(this Object obj)
        {
            return !IsNull(obj);
        }
        
        public static GameObject Spawn(Type type)
        {
            var obj = EEGameObjectUtils.Create(type);
            obj.AddComponent(type);
            return obj;
        }

        public static Component EETryGetComponent(GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component.IsNull()) component = null;
            return component;
        }
    }
}
