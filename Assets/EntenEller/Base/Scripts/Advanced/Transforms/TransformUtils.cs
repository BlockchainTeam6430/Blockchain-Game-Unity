using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms
{
    public static class TransformUtils
    {
        public static Transform GetFirstParent (this Transform target)
        {
            var parent = target.parent;
            while (parent.IsNotNull())
            {
                target = parent;
                parent = target.parent;
            }
            return target;
        }
        
        public static void SetParentResetPRS(this Transform target, Transform parent)
        {
            target.SetParent(parent);
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;
        }
        
        public static void SetParentSavePRS(this Transform target, Transform parent)
        {
            target.SetParent(parent);
        }

        public static List<Transform> GetFirstRowOfChildren(this Transform target)
        {
            var list = new List<Transform>();
            for (var i = 0; i < target.childCount; i++)
            {
                list.Add(target.GetChild(i));
            }
            return list;
        }
    }
}