namespace Plugins.EntenEller.Base.Scripts.UI.Text
{
    public class EETextSimple : EEText
    {
        protected override void EEAwake()
        {
            NewDataEvent += Change;
            base.EEAwake();
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            NewDataEvent -= Change;
        }

        protected override void Change()
        {
            Set(GetTranslated());
        }
    }
}
