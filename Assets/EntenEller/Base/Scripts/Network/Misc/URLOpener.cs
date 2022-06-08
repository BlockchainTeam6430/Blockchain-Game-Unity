using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Misc
{
    public class URLOpener : MonoBehaviour
    {
        public void Open(string url)
        {
            Application.OpenURL(url);
        }
    }
}
