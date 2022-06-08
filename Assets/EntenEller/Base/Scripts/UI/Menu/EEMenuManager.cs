using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Translation;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu
{
    [ExecutionOrder(3000)]
    public class EEMenuManager : EEBehaviour
    {
        public static event Action<string> GotNewNameEvent;
        [ReadOnly] public List<EEMenuData> History = new List<EEMenuData>();
        [SerializeField] private int historySize = -1;
        
        public void Add(EEMenuData menuData)
        {
            GotNewNameEvent.Call(menuData.Key);
            if (menuData.IsRecordHistory)
            {
                if (History.LastOrDefault() == menuData) return;
                if (historySize != -1 && historySize <= History.Count)
                {
                    History.Remove(History.First());
                }
                History.Add(menuData);
            }
            SetState(menuData, true);
        }
        
        public void Recreate(EEMenuData menuData)
        {
            HideAll(menuData);
            Add(menuData);
        }

        public void Remove(EEMenuData menuData)
        {
            if (menuData.IsRecordHistory)
            {
                if (History.Last() != menuData) return;
                History.Remove(menuData);
            }
            SetState(menuData, false);
        }
        
        public void Back()
        {
            if (History.Count <= 1)
            {
                Rebuild();
                return;
            }
            History.Remove(History.LastOrDefault());
            Rebuild();
        }
        
        public void CleanUntil(EEMenuData menuData)
        {
            for (var i = History.Count - 1; i >= 0; i--)
            {
                var menu = History[i];
                if (menu == menuData) break;
                Remove(menu);
            }
            Rebuild();
        }
        
        public void CleanAll()
        {
            History.Clear();
            HideAll();
        }
        
        public static void HideAll(EEMenuData menuData = null)
        {
            var allMenus = FindObjectsOfType<EEMenu>().Where(a => !a.IgnoreMenuSystem);
            if (menuData != null)
            {
                allMenus = allMenus.Where(a => !menuData.MenuEETags.Contains(a.GetSelf<EETagHolder>().FirstTag)).ToArray();
            }
            allMenus.ForEach(b => b.SetState(false));
        }
        
        private void Rebuild()
        {
            HideAll();
            foreach (var menuData in History)
            {
                if (menuData.MenuMode == MenuMode.Add) SetState(menuData, true);
                else
                {
                    HideAll();
                    SetState(menuData, true);
                }
            }
        }

        private static void SetState(EEMenuData menuData, bool isOn)
        {
            for (var i = 0; i < menuData.MenuEETags.Count; i++)
            {
                var eeTag = menuData.MenuEETags[i];
                EETagUtils.FindEETagInScenes(eeTag).GetSelf<EEMenu>().SetState(isOn);
            }
        }
    }
}
