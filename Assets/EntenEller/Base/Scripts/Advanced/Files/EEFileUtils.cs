using System.IO;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Files
{
    public static class EEFileUtils
    {
        public static void Save(byte[] bytes, string path)
        {
            File.WriteAllBytes(path, bytes);
        }

        public static byte[] Load(string path)
        {
            return File.ReadAllBytes(path);
        }

        public static bool IsExist(string path)
        {
            return File.Exists(path);
        }
    }
}
