using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class GameObjectStateObserver : EEBehaviour
    {
        public Action<bool> StateChangedEvent;
        public EESuperAction EnableSuperAction;
        public EESuperAction DisableSuperAction;
        
        protected override void EEEnable()
        {
            ActionUtils.Call(StateChangedEvent, true);
            EnableSuperAction.Call(ComponentID);
        }

        protected override void EEDisable()
        {
            ActionUtils.Call(StateChangedEvent, false);
            DisableSuperAction.Call(ComponentID);
        }
    }
}
