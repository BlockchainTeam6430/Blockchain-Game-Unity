using NodeSystem.Dialog;
using Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.UI;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Actions
{
    public class DialogPressActionsPlayer : EEBehaviour
    {
        protected override void EEAwake()
        {
            GetSelf<DialogUISpawnerPlayer>().PrefabSpawnedEvent += (node, poolObject) =>
            {
                var playerNode = (DialogPlayerNode) node;
                var connectedNodes = playerNode.GetListOfConnectedNodes(nameof(playerNode.OnPressActionNodes));
                connectedNodes.ForEach(a =>
                {
                    poolObject.GetChild<UnityEngine.UI.Button>().onClick.AddListener(Call);
                    void Call()
                    {
                        ((NodeSystem.Dialog.ActionNode) a).Call();
                    }
                });
            };
        }
    }
}
