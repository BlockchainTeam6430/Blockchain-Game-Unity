using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.UIShape
{
    [ExecuteInEditMode]
    public class EEEllipse : EEBehaviourLoop
    {
        public float LineWeight; 
        
        protected override void Loop()
        {
            base.Loop();
            SetValues();
        }
        
        private void SetValues()
        {
            GetSelf<Ellipse>().OutlineProperties.LineWeight = LineWeight;
            GetSelf<Ellipse>().SetAllDirty();
        }

        #if UNITY_EDITOR
        private void Update()
        {
            SetValues();
        }
        #endif
    }
}
