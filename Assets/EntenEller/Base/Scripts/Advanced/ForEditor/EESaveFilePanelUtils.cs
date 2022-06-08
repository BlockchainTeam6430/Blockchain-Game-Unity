
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Plugins.EntenEller.Base.Scripts.Advanced.ForEditor
{
    public static class EESaveFilePanelUtils
    {
#if UNITY_EDITOR
        public static bool TryToGetSavePath(object key, string name, string extension, out string pathToSave)
        {
            var keyS = key.ToString();
            var previousPath = PlayerPrefs.HasKey(keyS)
                ? PlayerPrefs.GetString(keyS)
                : Application.dataPath;
            pathToSave = EditorUtility.SaveFilePanel("Save dialog", previousPath, name, extension);
            if (pathToSave.Length == 0) return false;
            PlayerPrefs.SetString(keyS, pathToSave);
            pathToSave = "Assets" + pathToSave.Replace(Application.dataPath, "");
            EEDebug.Log(EEDebugTag.Editor, "Path = " + pathToSave);
            return true;
        }
#endif
    }
}
