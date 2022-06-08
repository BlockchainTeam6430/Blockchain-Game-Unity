using System;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using Plugins.EntenEller.Base.Scripts.Translation;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using Sirenix.OdinInspector;
using TMPro;

namespace Plugins.EntenEller.Base.Scripts.UI.Text
{
    public abstract class EEText : EEBehaviourEEMenu
    {
        [MultiLineProperty] public string Prefix;
        [MultiLineProperty] public string Postfix;
        [MultiLineProperty] public string Data;
        public Action NewDataEvent;

        public void SetPrefix(string prefix)
        {
            Prefix = prefix;
            Change();
        }

        public void SetPostfix(string postfix)
        {
            Postfix = postfix;
            Change();
        }

        public void SetData(string data)
        {
            Data = data;
            Change();
        }

        protected override void EEAwake()
        {
            base.EEAwake();
            Change();
        }

        protected override void MenuStartShow()
        {
            base.MenuStartShow();
            LanguageManager.Instance.SwitchedSuperAction.Event += Change;
        }

        protected override void MenuStartHide()
        {
            base.MenuStartHide();
            LanguageManager.Instance.SwitchedSuperAction.Event -= Change;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            LanguageManager.Instance.SwitchedSuperAction.Event -= Change;
        }

        protected abstract void Change();

        protected void Set(string text)
        {
            if (this == null) return;
           if (GetComponent<TextMeshProUGUI>()) GetComponent<TextMeshProUGUI>().text = text;
        }

        protected string GetTranslated()
        {
            return LanguageManager.Instance.GetTranslation(Prefix) +
                   LanguageManager.Instance.GetTranslation(Data) +
                   LanguageManager.Instance.GetTranslation(Postfix);
        }
    }
}