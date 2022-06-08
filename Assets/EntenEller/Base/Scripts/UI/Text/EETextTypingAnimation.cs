using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Text
{
    public class EETextTypingAnimation : EEText
    {
        private int currentStopIndex;
        private string current, target;
        [SerializeField] private FloatApproach Approach;

        protected override void EEAwake()
        {
            target = current = GetTranslated();
            Set(GetTranslated());
        }
        
        protected override void Change()
        {
            target = GetTranslated();
            
            var length = Mathf.Min(target.Length, current.Length);
            currentStopIndex = length;
            
            for (var i = 0; i < length; i++)
            {
                if (current[i] == target[i]) continue;
                currentStopIndex = i;
                break;
            }
            
            Approach.SetCurrent(current.Length);
            Approach.SetTarget(currentStopIndex);
        }
        
        protected override void MenuLoop()
        {
            base.MenuLoop();
            if (Approach.IsReached)
            {
                if (current != target) Approach.SetTarget(target.Length);
            }
            else
            {
                var index = Mathf.RoundToInt((int) Approach.Current);
                current = Approach.Target < Approach.Current ? current.Substring(0, index) : target.Substring(0, index);
                Set(current);
            }
            Approach.Proceed();
        }
    }
}
