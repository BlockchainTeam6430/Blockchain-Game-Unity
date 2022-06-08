using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;

namespace Plugins.EntenEller.Base.Scripts.Virtues
{
    [ExecutionOrder(9999)]
    public class VirtueHolder : EEBehaviourLoop
    {
        public float Value;
        public List<Virtue> Virtues = new List<Virtue>();
        public Action<float> ChangedValueEvent;
        public EESuperAction BecomeNotEmptySuperAction;
        public EESuperAction BecomeEmptySuperAction;
        private int previousAmount = 0;

        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEGameObject>().ChangedHierarchySuperAction.Event += Changed;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEGameObject>().ChangedHierarchySuperAction.Event -= Changed;
        }

        private void Changed()
        {
            Virtues = GetAllChild<Virtue>().Where(a => a.GetEEGameObject().IsActive).ToList();
            if (Virtues.Count == 0) BecomeEmptySuperAction.Call(ComponentID);
            else if (previousAmount == 0) BecomeNotEmptySuperAction.Call(ComponentID);
            previousAmount = Virtues.Count;
        }

        protected override void Loop()
        {
            base.Loop();
            CountValue();
        }

        private void CountValue()
        {
            var value = 0f;
            if (Virtues.Count != 0)
            {
                var valueNormal = CountNormal();
                value = valueNormal * 1;
            }
            if (Value.IsAlmostEqual(value)) return;
            Value = value;
            ChangedValueEvent.Call(Value);
        }
        
        private float CountNormal()
        {
            var sorted = Virtues.Where(a => !a.IsPercent);
            var max = Virtues.Max(a => a.Rank);
            sorted = sorted.Where(a => a.Rank == max);
            var sum = 0f;
            foreach (var virtue in sorted)
            {
                sum = virtue.Summary(sum);
            }
            return sum;
        }
        
        private float CountPercent()
        {
            return Virtues.Where(a => a.IsPercent).Sum(b => b.Summary(b.Value));
        }
    }
}