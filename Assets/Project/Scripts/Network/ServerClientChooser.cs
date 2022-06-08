using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.Scripts.Network
{
    [ExecutionOrder(-1)]
    public class ServerClientChooser : EEBehaviour
    {
        private static int index = -1;
        [SerializeField] private bool isServer;
        
        protected override void EEAwake()
        {
            base.EEAwake();

            if (index == -1)
            {
                index = isServer ? 0 : 1;
            }
            else
            {
                transform.GetChild(index).gameObject.SetActive(true);   
            }
        }
    }
}
