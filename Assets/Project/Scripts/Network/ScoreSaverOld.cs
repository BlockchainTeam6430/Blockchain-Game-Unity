using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EntenEller.Base.Scripts.Network.HTTP;
using Newtonsoft.Json;
using Plugins.EntenEller.Base.Scripts.Blockchain;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using Project.Scripts.Character;
using Project.Scripts.Network;
using UnityEngine;

namespace Project.Scripts
{
    public class ScoreSaverOld : EEBehaviour
    {
        private string pass0 = "AHGzpFuDFY6CqHVhCXtaRtacQHx2atf7e";
        [SerializeField] private string pass1;
        
        public void Send(bool result)
        {
            var password = pass0 + pass1;
            var mySHA256 = SHA256.Create();
            var key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
            var iv = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            var pointSystem = FindObjectsOfType<CharacterMover>(true).First().GetSelf<PointSystem>();

            var obj = new ScoreLoader.ScoreData
            {
                Wallet = Wallet.Account,
                Points = pointSystem.points,
                Timer = DateTime.Now.Ticks,
                is_pvp = 0,
                Wins = result ? 1 : 0,
                Loses = result ? 0 : 1
            };

            var json = JsonConvert.SerializeObject(obj);
            var encrypted = SimpleAESEncryption.EncryptString(json, key, iv);

            var dictionary = new Dictionary<string, string>
            {
                {"data", encrypted}
            };

            GetSelf<EEHTTPStringSender>().SetNewData(dictionary);
            GetSelf<EEHTTP>().Call();
        }
    }
}