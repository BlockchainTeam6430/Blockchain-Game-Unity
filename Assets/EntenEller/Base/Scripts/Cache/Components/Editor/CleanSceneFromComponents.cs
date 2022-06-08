using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Editor
{
    public static class CleanSceneFromComponents 
    {
        [MenuItem("Window/EntenEller/Clean Scene From Components")]
        private static void Clean()
        {
            Object.FindObjectsOfType<CachedComponent>().ForEach(a => Object.DestroyImmediate(a));
        }
    }
}
