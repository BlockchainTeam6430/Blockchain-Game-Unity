using EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Timers;
using Project.Scripts.Network.Lobby;
using UnityEngine;

namespace Project.Scripts
{
    public class Restarter : EEBehaviourLoop
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            print(Application.dataPath + "/../../server.x86_64");
            GetSelf<EEHTTP>().url = LobbySubserver.URLPHP + "get.php";
            GetSelf<EEHTTPStringReceiver>().SuccessEvent += data =>
            {
                var amount = int.Parse(data);
                print(amount);
                for (var i = amount; i > 0; i--)
                {
                    var psi = new System.Diagnostics.ProcessStartInfo() { FileName = Application.dataPath + "/../../server.x86_64", UseShellExecute = true };
                    var j = i;
                    EETime.StartTimer(new EETime.EETimerData
                    {
                        Action = () =>
                        {
                            System.Diagnostics.Process.Start(psi);
                        },
                        FinalTime = 3 * j
                    });
                }
            };
        }
    }
    
    
}
