using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Pathes
{
    public static class Path
    {
        public static string AbsolutePathToAssetPath(string absolutePath)
        {
            return "Assets" + absolutePath.Substring(Application.dataPath.Length);
        }
    }
}
