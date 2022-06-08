using UnityEngine.EventSystems;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    public class EEPointerUI : EEPointer, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            IsFocused = true;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            IsFocused = false;
        }
    }
}
