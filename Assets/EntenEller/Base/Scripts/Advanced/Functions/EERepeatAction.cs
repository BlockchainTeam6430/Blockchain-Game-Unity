namespace Plugins.EntenEller.Base.Scripts.Advanced.Functions
{
    public static class EERepeatAction
    {
        public static void Repeat(System.Action action, int amount)
        {
            for (var i = 0; i < amount; i++) action.Invoke();
        }
    }
}
