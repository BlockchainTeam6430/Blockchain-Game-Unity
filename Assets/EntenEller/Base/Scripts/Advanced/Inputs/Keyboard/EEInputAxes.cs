using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Keyboard
{
    public class EEInputAxes : EEBehaviourLoop
    {
        public event Action<Axe> AxeDown;
        public event Action<Axe> AxeUp;
        public event Action<Axe> AxeClick;
        private IEnumerable<Axe> axes;
        private readonly List<Axe> axesDownState = new List<Axe>();

        public enum Axe
        {
            Vertical,
            Horizontal,
            Action,
            Menu
        }

        public static float GetValue(Axe axe)
        {
            return Input.GetAxis(axe.ToString());
        }

        public static bool GetState(Axe axe)
        {
            return !GetValue(axe).IsAlmostZero();
        }

        protected override void EEAwake()
        {
            base.EEAwake();
            axes = Enum.GetValues(typeof(Axe)).Cast<Axe>();
        }

        protected override void Loop()
        {
            base.Loop();
            foreach (var axe in axes)
            {
                if (GetState(axe))
                {
                    if (axesDownState.Contains(axe)) continue;
                    axesDownState.Add(axe);
                    AxeDown.Call(axe);
                }
                else
                {
                    if (!axesDownState.Contains(axe)) continue;
                    axesDownState.Remove(axe);
                    AxeUp.Call(axe);
                    AxeClick.Call(axe);
                }
            }
        }
    }
}
