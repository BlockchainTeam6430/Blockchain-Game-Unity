using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.EditorOnly
{
    [ExecuteInEditMode]
    public class ForceLossyScale : EEBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Vector3 scale = new Vector3(1, 1, 1);

        private void Update()
        {
            var factor = scale;
            var ts = GetSelf<Transform>();
            
            while(true)
            {
                var tParent = ts.parent;
                if (tParent.IsNull()) break;
                factor.x /= tParent.localScale.x;
                factor.y /= tParent.localScale.y;
                factor.z /= tParent.localScale.z;
                ts = tParent;
            }

            GetSelf<Transform>().localScale = factor;
        }
#endif
    }
}