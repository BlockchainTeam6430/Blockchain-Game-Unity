using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.World
{
    public class WorldMenuRotation : EEBehaviourLoop
    {
        protected override void Loop()
        {
            if (GetSelf<WorldMenu>().MainCamera is null) return;
            GetSelf<Transform>().rotation = Quaternion.LookRotation(GetSelf<Transform>().position - GetSelf<WorldMenu>().MainCamera.transform.position);
        }
    }
}