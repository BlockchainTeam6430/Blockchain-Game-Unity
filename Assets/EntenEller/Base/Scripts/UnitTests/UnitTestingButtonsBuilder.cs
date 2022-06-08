using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;

namespace Plugins.EntenEller.Base.Scripts.UnitTests
{
    public class UnitTestingButtonsBuilder : EESpawnerEEGameObject
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            UnitTestingClassAndFunctionGetter.GotDataEvent += (test, type, method) =>
            {
                var button = Spawn();
                button.GetSelf<UnitTestButton>().Setup(test, type, method);
            };
        }
    }
}
