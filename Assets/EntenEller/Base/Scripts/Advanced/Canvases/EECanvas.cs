using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Timers;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Canvases
{
    public class EECanvas : EEBehaviour
    {
        private static readonly List<EECanvas> canvases = new List<EECanvas>();

        protected override void EEEnable()
        {
            canvases.Add(this);
            GetSelf<EEMenuView>().StartShowSuperAction.Event += On;
            GetSelf<EEMenuView>().FinishHideSuperAction.Event += Off;
            EETime.StartTimer(new EETime.EETimerData
            {
                Action = () =>
                {
                    if (GetSelf<EEMenu>().IsActive) On();
                    else Off();
                },
                FinalTime = 0.1f,
                ComponentID = ComponentID
            });
        }

        protected override void EEDisable()
        {
            canvases.Remove(this);
            GetSelf<EEMenuView>().StartShowSuperAction.Event -= On;
            GetSelf<EEMenuView>().FinishHideSuperAction.Event -= Off;
            EETime.StopAllTimersForComponentID(ComponentID);
        }

        private void On()
        {
            SetState(true);
        }

        private void Off()
        {
            SetState(false);
        }

        private void SetState(bool isOn)
        {
            GetSelf<Canvas>().enabled = isOn;
            if (GetSelf<CanvasScaler>()) GetSelf<CanvasScaler>().enabled = isOn;
            if (GetSelf<GraphicRaycaster>()) GetSelf<GraphicRaycaster>().enabled = isOn;
            if (GetSelf<Animator>()) GetSelf<Animator>().enabled = isOn;
        }

        public void SetZOrder(int order)
        {
            GetSelf<Canvas>().sortingOrder = order;
        }
        
        public void SetZOrderHigher()
        {
            GetSelf<Canvas>().sortingOrder += 1;
        }
        
        public void SetZOrderLower()
        {
            GetSelf<Canvas>().sortingOrder -= 1;
        }
        
        public void DisableRaycast()
        {
            GetSelf<GraphicRaycaster>().enabled = false;
        }
        
        public void EnableRaycast()
        {
            GetSelf<GraphicRaycaster>().enabled = true;
        }

        public static bool CheckHitInCanvas(Vector2 screenPosition)
        {
            var pointerData = new PointerEventData(EventSystem.current);
            var results = new List<RaycastResult>();
            foreach (var canvas in canvases)
            {
                pointerData.position = screenPosition;
                canvas.GetSelf<GraphicRaycaster>().Raycast(pointerData, results);
                if (results.Any()) return true;
            }
            return false;
        }
    }
}