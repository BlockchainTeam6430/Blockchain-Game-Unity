using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

namespace Project.Scripts.UI
{
    public class SignupButton : EEBehaviourLoop
    {
        [SerializeField] private TMP_InputField input0, input1;

        protected override void Loop()
        {
            base.Loop();
            GetSelf<Button>().interactable = false;
            if (input0.text.IsNullOrEmpty()) return;
            if (input1.text.IsNullOrEmpty()) return;
            if (input1.text != input0.text) return;
            if (input1.text.Length < 6) return;
            GetSelf<Button>().interactable = true;
        }
    }
}
