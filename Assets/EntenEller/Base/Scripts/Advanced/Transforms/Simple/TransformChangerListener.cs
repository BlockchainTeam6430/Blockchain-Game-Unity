using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class TransformChangerListener : EEBehaviour
    {
        public EESuperAction StopSuperAction, StartSuperAction;
        private bool isMoving, isRotating, isScaling;
        [ReadOnly] public bool IsChanging;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            
            GetSelf<TransformApproachPosition>().Position.StartSuperAction.Event += StartPosition;
            GetSelf<TransformApproachRotation>().Rotation.StartSuperAction.Event += StartRotation;
            GetSelf<TransformApproachScale>().Scale.StartSuperAction.Event += StartScale;
            
            GetSelf<TransformApproachPosition>().Position.StopSuperAction.Event += StopPosition;
            GetSelf<TransformApproachRotation>().Rotation.StopSuperAction.Event += StopRotation;
            GetSelf<TransformApproachScale>().Scale.StopSuperAction.Event += StopScale;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            
            GetSelf<TransformApproachPosition>().Position.StartSuperAction.Event -= StartPosition;
            GetSelf<TransformApproachRotation>().Rotation.StartSuperAction.Event -= StartRotation;
            GetSelf<TransformApproachScale>().Scale.StartSuperAction.Event -= StartScale;
            
            GetSelf<TransformApproachPosition>().Position.StopSuperAction.Event -= StopPosition;
            GetSelf<TransformApproachRotation>().Rotation.StopSuperAction.Event -= StopRotation;
            GetSelf<TransformApproachScale>().Scale.StopSuperAction.Event -= StopScale;
        }

        private void StartPosition()
        {
            isMoving = true;
            TransformChange();
        }
        
        private void StartRotation()
        {
            isRotating = true;
            TransformChange();
        }

        private void StartScale()
        {
            isScaling = true;
            TransformChange();
        }
        
        private void StopPosition()
        {
            isMoving = false;
            TransformChange();
        }

        private void StopRotation()
        {
            isRotating = false;
            TransformChange();
        }
        
        private void StopScale()
        {
            isScaling = false;
            TransformChange();
        }

        private void TransformChange()
        {
            var isChanging = isMoving || isRotating || isScaling;
            if (IsChanging == isChanging) return;
            IsChanging = isChanging;
            if (IsChanging) StartSuperAction.Call(ComponentID);
            else StopSuperAction.Call(ComponentID);
        }
    }
}
