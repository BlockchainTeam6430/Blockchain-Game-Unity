using System;
using NodeSystem.Dialog;
using Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.UI;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Actions
{
    public class DialogContinueActionsPlayer : EEBehaviour
    {
        public event Action<DialogNode, EEGameObject> DialogContinuedEvent;
        public event Action<DialogNode, EEGameObject> DialogEndedEvent;
        
        protected override void EEAwake()
        {
            GetSelf<DialogUISpawnerPlayer>().PrefabSpawnedEvent += (node, obj) =>
            {
                var button = obj.GetChild<Button>();
                var connectedNodes = node.GetListOfConnectedNodes(nameof(node.Output));
                if (connectedNodes.Count == 0)
                {
                    button.onClick.AddListener(End);
                    void End()
                    {
                        EEDebug.Log(EEDebugTag.Dialog, "Ended " + node.name);
                        DialogEndedEvent.Call(node, obj);
                        EESingleton.Get<Dialog>().EndDialog();
                    }
                }
                else
                {
                    button.onClick.AddListener(Continue);
                    void Continue()
                    {
                        EEDebug.Log(EEDebugTag.Dialog, "Continued " + node.name);
                        DialogContinuedEvent.Call(node, obj);
                        EESingleton.Get<Dialog>().ContinueDialog((DialogPlayerNode) node);
                    }
                }
            };
        }
    }
}