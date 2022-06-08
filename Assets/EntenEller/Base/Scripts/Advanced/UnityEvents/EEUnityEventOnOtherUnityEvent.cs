using System.Collections.Generic;
using System.Linq;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public class EEUnityEventOnOtherUnityEvent : EEVariableFinder
    {
        //TODO make it public
        [SerializeField] private EESuperAction superAction;
        [ReadOnly] [SerializeField] private List<EESuperAction> actions = new List<EESuperAction>();

        protected override void EEAwake()
        {
            base.EEAwake();
            LoopType = Segment.Invalid;
            foreach (var data in VariablesInfo.SelectMany(a => a.Variables))
            {
                actions.Add(data.Value as EESuperAction);
            }
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            foreach (var action in actions)
            {
                action.Event += Action;
            }
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            foreach (var action in actions)
            {
                action.Event -= Action;
            }
        }

        private void Action()
        {
            superAction.Call(ComponentID);
        }
    }
}
