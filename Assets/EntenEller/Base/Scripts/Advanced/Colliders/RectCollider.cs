using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Colliders
{
    public class RectCollider : EEBehaviourEEMenu
    {
        [SerializeField] private float dX0 = 0;
        [SerializeField] private float dX1 = 0;
        [SerializeField] private float dY0 = 0;
        [SerializeField] private float dY1 = 0;
        [SerializeField] private bool isSetOnMenuStartShowOnly = true;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<BoxCollider2D>().enabled = GetParent<EEMenu>().IsActive;
        }
        
        protected override void MenuStartShow()
        {
            base.MenuStartShow();
            GetSelf<BoxCollider2D>().enabled = true;
            ChangeColliderSize();
        }

        protected override void MenuStartHide()
        {
            base.MenuStartHide();
            GetSelf<BoxCollider2D>().enabled = false;
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            if (isSetOnMenuStartShowOnly) return;
            ChangeColliderSize();
        }

        [Button]
        private void ChangeColliderSize()
        {
            var rect = RectTransformUtility.PixelAdjustRect(GetSelf<RectTransform>(), GetParent<Canvas>());
            var size = new Vector2(rect.width, rect.height);
            size.x += dX0 + dX1;
            size.y += dY0 + dY1;
            var offset = rect.center;
            offset.x -= dX0 / 2 - dX1 / 2;
            offset.y -= dY0 / 2 - dY1 / 2;
            GetSelf<BoxCollider2D>().size = size;
            GetSelf<BoxCollider2D>().offset = offset;
        }
    }
}
