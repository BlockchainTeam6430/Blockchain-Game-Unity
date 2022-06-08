namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    public class EEPointerObject : EEPointer
    {
        private void OnMouseDown()
        {
            IsFocused = true;
        }
        
        private void OnMouseUp()
        {
            IsFocused = false;
        }
    }
}
