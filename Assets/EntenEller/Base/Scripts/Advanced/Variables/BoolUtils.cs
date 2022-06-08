namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class BoolUtils
    {
        public static bool StringToBool (string str)
        {
            return str.ToLower() == "true";
        }
    }
}