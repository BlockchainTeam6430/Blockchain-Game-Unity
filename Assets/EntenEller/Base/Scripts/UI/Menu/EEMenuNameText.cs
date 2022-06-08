using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.UI.Text;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu
{
    public class EEMenuNameText : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            EEMenuManager.GotNewNameEvent += GotNewName;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EEMenuManager.GotNewNameEvent -= GotNewName;
        }

        private void GotNewName(string menuName)
        {
            GetSelf<EEText>().SetData(menuName);
        }
    }
}
