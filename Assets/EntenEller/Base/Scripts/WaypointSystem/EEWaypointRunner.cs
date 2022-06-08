using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.SimpleActions;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.WaypointSystem
{
    public class EEWaypointRunner : EEBehaviourLoop
    {
        public EEWaypointSystem WaypointSystem;
        public int Index = 0;
        public int TargetIndex = -1;
        public int Direction = 1;
        public EESuperAction PointReachedSuperAction;
        public EESuperAction StartRunSuperAction;
        public EESuperAction StopRunSuperAction;
        public EESuperAction EndRunSuperAction;
        public bool IsOn = true;
        public bool IsLoop;

        public void SetWaypointSystem(EEWaypointSystem waypointSystem)
        {
            Index = 0;
            On();
            WaypointSystem = waypointSystem;
        }
        
        protected override void Loop()
        {
            base.Loop();
            if (WaypointSystem.IsNull()) return;
            if (!IsOn) return;
            var index = Index;
            var pos = WaypointSystem.GetNext(GetSelf<Transform>().position, ref Index, ref Direction, out var isEnd, TargetIndex, IsLoop);
            if (isEnd && IsLoop == false)
            {
                Off();
                EndRunSuperAction.Call(ComponentID);
                return;
            }
            if (index != Index) PointReachedSuperAction.Call(ComponentID);
            GetSelf<TransformApproachPosition>().Position.SetTarget(pos);
        }

        public void On()
        {
            IsOn = true;
            StartRunSuperAction.Call(ComponentID);
        }

        public void Off()
        {
            IsOn = false;
            StopRunSuperAction.Call(ComponentID);
            GetSelf<TransformApproachPosition>().Position.Off();
        }
    }
}
