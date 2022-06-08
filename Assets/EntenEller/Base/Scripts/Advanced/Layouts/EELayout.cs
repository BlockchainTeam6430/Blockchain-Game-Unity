using System.Collections;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Layouts
{
    public class EELayout : EEBehaviour
    {
        private IEnumerator Start()
        {
            yield return null;
            GetSelf<LayoutGroup>().enabled = false;
            yield return null;
            GetSelf<LayoutGroup>().enabled = true;
        }
    }
}
