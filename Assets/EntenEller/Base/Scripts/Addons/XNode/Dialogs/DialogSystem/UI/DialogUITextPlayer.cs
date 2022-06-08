using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.UI
{
    public class DialogUITextPlayer : EEReadyBehaviour
    {
        protected override void EEAwake()
        {
            GetSelf<DialogUISpawnerPlayer>().PrefabSpawnedEvent += (node, poolObject) =>
            {
                var text = node.Text;
                if (text == null) text = "[End Dialog]";
                if (text == string.Empty) text = "[Continue]";
                poolObject.GetChild<Text>().text = text;
            };
        }
    }
}