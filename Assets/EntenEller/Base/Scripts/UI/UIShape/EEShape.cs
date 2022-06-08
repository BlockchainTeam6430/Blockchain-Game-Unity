using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.UI.UIShape
{
    [ExecuteInEditMode]
    public class EEShape : EEBehaviourLoop
    {
        public Color FillColor = Color.white;
        private Color fillColorOld;
        public Color OutlineColor = Color.black;
        private Color outlineColorOld;
        
        protected override void Loop()
        {
            SetValues();
        }
        
        private void SetValues()
        {
            if (FillColor != fillColorOld)
            {
                fillColorOld = FillColor;
                if (GetSelf<Rectangle>()) GetSelf<Rectangle>().ShapeProperties.FillColor = FillColor;
                if (GetSelf<Ellipse>()) GetSelf<Ellipse>().ShapeProperties.FillColor = FillColor;
                if (GetSelf<Polygon>()) GetSelf<Polygon>().ShapeProperties.FillColor = FillColor;
                GetSelf<Graphic>().SetAllDirty();
            }
            if (OutlineColor != outlineColorOld)
            {
                outlineColorOld = OutlineColor;
                if (GetSelf<Rectangle>()) GetSelf<Rectangle>().ShapeProperties.OutlineColor = OutlineColor;
                if (GetSelf<Ellipse>()) GetSelf<Ellipse>().ShapeProperties.OutlineColor = OutlineColor;
                if (GetSelf<Line>()) GetSelf<Line>().ShapeProperties.FillColor = OutlineColor;
                GetSelf<Graphic>().SetAllDirty();
            }
        }
    }
}
