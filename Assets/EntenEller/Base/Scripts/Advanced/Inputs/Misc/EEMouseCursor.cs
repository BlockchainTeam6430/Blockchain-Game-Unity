using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Misc
{
    public class EEMouseCursor : EEBehaviour
    {
        public bool IsLocked { get; private set; }
        public EESuperAction LockedSuperAction;
        public EESuperAction UnlockedSuperAction;

        public void Lock()
        {
            IsLocked = true;
            LockedSuperAction.Call(ComponentID);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Unlock()
        {
            IsLocked = false;
            UnlockedSuperAction.Call(ComponentID);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
