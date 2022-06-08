using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Applications
{
    public class EEFPS : MonoBehaviour
    {
        public void Set(int fps)
        {
            Application.targetFrameRate = fps;
        }
    }
}
