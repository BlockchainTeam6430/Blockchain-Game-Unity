using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem
{
    public class DialogView : EEBehaviour
    {
        protected override void EEAwake()
        {
            GetParent<Dialog>().DialogStartedEvent += path =>
            {
               // GetSelf<EEMenu>().Show();
            };
            GetParent<Dialog>().DialogEndedEvent += () =>
            {
               // GetSelf<EEMenu>().Hide();
            };
        }
    }
}
