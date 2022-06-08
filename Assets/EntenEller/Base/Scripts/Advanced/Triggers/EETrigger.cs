using System;
using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Triggers
{
    [ExecutionOrder(9999)]
    [RequireComponent(typeof(EETagHolder))]
    public abstract class EETrigger : EEBehaviour
    {
        public bool IsColliding => RightCollisions.Count != 0;
        public List<string> CollidingTags = new List<string>();
        
        [ReadOnly] public List<EETrigger> RightCollisions = new List<EETrigger>();
        [ReadOnly] public List<EETrigger> WrongCollisions = new List<EETrigger>();
        
        public EESuperAction EnterRightSuperAction;
        public EESuperAction ExitRightSuperAction;
        public EESuperAction EnterWrongSuperAction;
        public EESuperAction ExitWrongSuperAction;
        public EESuperAction EnterSuperAction;
        public EESuperAction ExitSuperAction;
        
        public event Action<EETrigger> EnterRightEvent;
        public event Action<EETrigger> ExitRightEvent;
        public event Action<EETrigger> EnterWrongEvent;
        public event Action<EETrigger> ExitWrongEvent;
        public event Action<EETrigger> EnterEvent;
        public event Action<EETrigger> ExitEvent;

        public void Add(EETrigger trigger, bool isRight)
        {
            if (isRight)
            {
                EnterRightEvent.Call(trigger);
                EnterRightSuperAction.Call(ComponentID);
                RightCollisions.Add(trigger);
            }
            else
            {
                EnterWrongEvent.Call(trigger);
                EnterWrongSuperAction.Call(ComponentID);
                WrongCollisions.Add(trigger);
            }
            EnterEvent.Call(trigger);
            EnterSuperAction.Call(ComponentID);
        }

        public void Remove(EETrigger trigger,bool isRight)
        {
            if (isRight)
            {
                ExitRightEvent.Call(trigger);
                ExitRightSuperAction.Call(ComponentID);
                RightCollisions.Remove(trigger);
            }
            else
            {
                ExitWrongEvent.Call(trigger);
                ExitWrongSuperAction.Call(ComponentID);
                WrongCollisions.Remove(trigger);
            }
            ExitEvent.Call(trigger);
            ExitSuperAction.Call(ComponentID);
        }
    }
}