using System;
using System.Runtime.InteropServices;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Blockchain
{
    public class Wallet : EEBehaviour
    {
        public EESuperAction ConnectedSuperAction, SkipSuperAction;
        
        public static string Account = string.Empty;
        public static bool IsConnected = false;
        
        [DllImport("__Internal")]
        private static extern void EELogin();
        
        [DllImport("__Internal")]
        private static extern string EEGetWalletName();
        
        
        public void OnLogin()
        {
            #if UNITY_EDITOR
                Account = "guest_" + Guid.NewGuid();
                ConnectedSuperAction.Call();
                return;
            #endif
            EELogin();
            OnConnected();
        }

        private async void OnConnected()
        {
            while (string.IsNullOrEmpty(Account))
            {
                await new WaitForSeconds(1f);
                Account = EEGetWalletName();
                print(Account);
            }
            IsConnected = true;
            ConnectedSuperAction.Call();
        }

        public void Skip()
        {
            Account = "guest_" + Guid.NewGuid();
            SkipSuperAction.Call();
        }
    }
}
