using Plugins.EntenEller.Base.Scripts.Advanced.ForEditor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Plugins.EntenEller.Base.Scripts.Advanced.Prefabs
{
    public class EEPrefabCopier : MonoBehaviour
    {
#if UNITY_EDITOR
        [Button(ButtonSizes.Large)]
        [GUIColor(0, 1, 0.45f)]
        private void Copy()
        {
            var result = EESaveFilePanelUtils.TryToGetSavePath("PrefabCopy", name,"prefab", out var pathToSave);
            if (!result) return;
            if (pathToSave.Substring(0, 7) != "Assets/")
            {
                EEInfoWindowUtils.ShowInfoWindow("Error!", "Must be an 'Assets/' folder!");
                return;
            }
            var variant = PrefabUtility.InstantiatePrefab(gameObject) as GameObject;
            DestroyImmediate(variant.GetComponent<EEPrefabCopier>(), true);
            var saved = PrefabUtility.SaveAsPrefabAsset(variant, pathToSave);
            DestroyImmediate(variant);
        }
#endif
    }
}