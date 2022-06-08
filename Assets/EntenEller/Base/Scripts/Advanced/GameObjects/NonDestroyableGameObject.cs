using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class NonDestroyableGameObject : EEBehaviour
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
