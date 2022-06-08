using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.UnitTests
{
    public class UnitTestButtonTestAll : EEBehaviour
    {
        private readonly List<UnitTestButton> list = new List<UnitTestButton>();
        private bool isFail;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<CanvasGroup>().alpha = 0;
            UnitTestButton.CreatedEvent += Create;
            GetSelf<Button>().onClick.AddListener(Click);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            UnitTestButton.CreatedEvent -= Create; 
            GetSelf<Button>().onClick.RemoveListener(Click);
        }

        private void Create(UnitTestButton button)
        {
            list.Add(button);
            GetSelf<CanvasGroup>().alpha = 1;
        }

        private void Click()
        {
            list.ForEach(a => a.GetSelf<Button>().onClick.Invoke());
            var i = 0;
            list.ForEach(a => a.TestCompleted.Event += () =>
            {
                GetChild<Rectangle>().ShapeProperties.FillColor = UnitTestButton.ColorInProgress;
                i++;
                if (isFail) return;
                if (a.IsFail)
                {
                    isFail = true;
                    GetChild<Rectangle>().ShapeProperties.FillColor = UnitTestButton.ColorFail;
                }
                else if (i == list.Count)
                {
                    GetChild<Rectangle>().ShapeProperties.FillColor = UnitTestButton.ColorSuccess;
                } 
                GetChild<Rectangle>().ForceMeshUpdate();
            });
        }
    }
}
