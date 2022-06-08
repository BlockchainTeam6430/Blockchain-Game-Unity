using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.UI
{
    public class DialogUINpcDescription : EEReadyBehaviour
    {
      /*  [SerializeField] private Rectangle npcPortrait = null;
        [SerializeField] private Text npcName = null;

        protected override void EEAwake()
        {
            GetSelf<DialogNodeSorterNpc>().ReadyEvent += message =>
            {
                var node = GetSelf<DialogNodeSorterNpc>().DialogNpcNode;
                CharacterDescriptionNode firstNode = null;// node.GetListOfConnectedNodes(nameof(node.CharacterDescriptionNode)).FirstOrDefault();
                if (firstNode is null)
                {
                    Ready();
                    return;
                }
                var characterDescription = ((CharacterDescriptionNode) firstNode).CharacterDescription;
                npcName.text = characterDescription.Name;
                npcPortrait.GetComponent<RectangleSpriteChanger>().Change(characterDescription.Texture2D);
                Ready();
            };
        } */
    }
}
