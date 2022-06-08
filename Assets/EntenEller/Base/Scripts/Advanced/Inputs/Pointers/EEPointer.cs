using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    public abstract class EEPointer : EEBehaviour
    {
        [ReadOnly] public bool IsFocused;
        [ReadOnly] public bool IsDown;
        public EESuperAction DownSuperAction, UpSuperAction, ClickSuperAction, DragSuperAction;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            var mainPointer = EESingleton.Get<EEPointerManager>().PointersData.First();
            mainPointer.RawUpEvent += Up;
            mainPointer.RawDownEvent += Down;
            mainPointer.RawClickEvent += Click;
            mainPointer.RawDragEvent += Drag;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            var mainPointer = EESingleton.Get<EEPointerManager>().PointersData.First();
            mainPointer.RawUpEvent -= Up;
            mainPointer.RawDownEvent -= Down;
            mainPointer.RawClickEvent -= Click;
            mainPointer.RawDragEvent -= Drag;
        }

        private void Up()
        {
            IsDown = false;
            UpSuperAction.Call(ComponentID);
        }

        private void Down()
        {
            if (!IsFocused) return;
            IsDown = true;
            DownSuperAction.Call(ComponentID);
        }

        private void Click()
        {
            if (IsFocused) ClickSuperAction.Call(ComponentID);
        }

        private void Drag(Vector2 delta)
        {
            if (IsFocused) DragSuperAction.Call(ComponentID);
        }
    }
}
