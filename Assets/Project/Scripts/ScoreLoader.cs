using System;
using System.Collections.Generic;
using System.Linq;
using EntenEller.Base.Scripts.Network.HTTP;
using Newtonsoft.Json;
using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class ScoreLoader : EEBehaviour
    {
        [SerializeField] private EESpawner spawner;
        [SerializeField] private int i0, i1;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEHTTPStringReceiver>().SuccessEvent += Recieve;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEHTTPStringReceiver>().SuccessEvent -= Recieve;
        }

        private void Recieve(string data)
        {
            if (data == String.Empty) return;
            var list = JsonConvert.DeserializeObject<Dictionary<string, ScoreData>>(data).OrderBy(a => a.Value.Points).Reverse().ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var scoreData = list[i].Value;
                var obj = spawner.Spawn();
                var texts = obj.GetAllChild<EEText>();
                texts[0].SetData((i+1).ToString());
                var key = list[i].Key;
                if (i0 + i1 != 0 && key.Length > i0 + i1)
                {
                    texts[1].SetData(key.Substring(0, i0) + "..." + key.Substring(key.Length - i1));
                }
                else
                {
                    texts[1].SetData(key);
                }
                texts[2].SetData(scoreData.Loses.ToString());
                texts[3].SetData(scoreData.Wins.ToString());
                texts[4].SetData(scoreData.Games.ToString());
                texts[5].SetData(scoreData.max_points.ToString());
                texts[6].SetData(scoreData.Points.ToString());
            }
        }

        [Serializable]
        public class ScoreData
        {
            public string Wallet;
            public int Points;
            public long Timer;
            public int is_pvp;
            public int Wins;
            public int Loses;
            public int Games;
            public int max_points;
        }
    }
}
