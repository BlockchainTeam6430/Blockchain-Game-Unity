using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Behaviours
{
    public class EEBehaviourValues : MonoBehaviour
    {
        private static int idGlobal = int.MinValue;
        private int _ComponentID = int.MinValue;
        
        public int ComponentID
        {
            get
            {
                if (_ComponentID > int.MinValue) return _ComponentID;
                idGlobal++;
                _ComponentID = idGlobal;
                return _ComponentID;
            }
        }
    }
}