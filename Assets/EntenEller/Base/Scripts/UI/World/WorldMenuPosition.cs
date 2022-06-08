using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Colliders;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.World
{
    public class WorldMenuPosition : EEBehaviourLoop
    {
        private const float offset = 1.5f;
        private const float lerpSpeed = 0.1f;
        private Vector3 positionToMove;

        protected override void Loop()
        {
            base.Loop();
            if (GetSelf<WorldMenu>().MainCamera.IsNull()) return;
            if (!GetChild<EEMenu>().IsActive)
            {
                MoveToDefaultPosition();
                return;
            }
            if (!GetSelf<CurrentCameraBordersObserver>().IsOnScreen)
            {
                CountPositionToMove();
            }
            MoveToNewPosition();
        }

        private void CountPositionToMove()
        {
            positionToMove = GetSelf<WorldMenu>().MainCamera.transform.position + GetSelf<WorldMenu>().MainCamera.transform.forward * offset;
        }

        private void MoveToNewPosition()
        {
            GetSelf<Transform>().position = Vector3.Lerp(GetSelf<Transform>().position, positionToMove, lerpSpeed);
        }

        private void MoveToDefaultPosition()
        {
            GetSelf<Transform>().position = GetSelf<WorldMenu>().MainCamera.transform.position - GetSelf<WorldMenu>().MainCamera.transform.right * 10;
        }
    }
}