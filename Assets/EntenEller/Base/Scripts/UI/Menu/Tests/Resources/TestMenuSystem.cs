using System.Collections.Generic;
using System.Linq;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using Plugins.EntenEller.Base.Scripts.UnitTests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu.Tests.Resources
{
    public class TestMenuSystem : EEBehaviourTest
    {
        [SerializeField] private EEMenuData starting;
        [SerializeField] private EEMenuData add1;
        [SerializeField] private EEMenuData add2;
        [SerializeField] private EEMenuData recreate;
        [SerializeField] private EEMenuData noHistory;
        
        public IEnumerator<float> TestMenu()
        {
            starting.CleanAll();
            yield return Timing.WaitForOneFrame;
            starting.On();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            
            recreate.On();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            
            recreate.Off();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            
            recreate.On();
            add1.Off();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            
            add1.On();
            add2.On();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 4);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add2);

            starting.Back();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 3);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add1);
            
            starting.Back();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            
            starting.Back();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), starting);
            
            starting.Back();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            
            starting.Back();
            starting.On();
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(starting.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            
            recreate.On();
            add1.On();
            add2.On();

            Assert.AreEqual(EETagUtils.FindEETagInScenes(starting.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(recreate.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add1.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add2.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            add1.CleanUntil();
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(starting.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(recreate.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add1.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add2.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 3);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add1);
            
            starting.CleanAll();
            
            starting.On();
            starting.On();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);

            add1.On();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add1);

            add1.Off();
            add1.Off();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), starting);
            
            starting.CleanAll();
            
            starting.On();
            recreate.On();
            noHistory.On();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            Assert.AreEqual(EETagUtils.FindEETagInScenes(noHistory.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            noHistory.Off();
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            Assert.AreEqual(EETagUtils.FindEETagInScenes(noHistory.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
        }
    }
}
