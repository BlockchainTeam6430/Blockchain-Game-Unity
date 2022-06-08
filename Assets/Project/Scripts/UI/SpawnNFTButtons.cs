using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Blockchain.NFT;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class SpawnNFTButtons : EEBehaviour
    {
        [SerializeField] private NFTCollection nftCollection;
        
        public class Response 
        {
            public string Name;
            public string Image;
        }
        
        public async void Call()
        {
            for (var i = 0; i < nftCollection.Collection.Length; i++)
            {
                var nftData = nftCollection.Collection[i];
                var obj = GetSelf<EESpawnerEEGameObject>().Spawn();
                
                obj.GetSelf<Toggle>().group = GetSelf<ToggleGroup>();
                obj.GetSelf<NFTImageToggle>().ID = nftData.TokenId;
                var uri = await ERC721.URI(nftCollection.Chain, nftCollection.Network, nftCollection.Contract,
                    nftData.TokenId);
                uri = uri.Replace("assets", "images") + ".png";
                
                var http = EETagUtils.FindEETagInChildren(obj, "2").GetSelf<EEHTTP>();
                http.ChangeURL(uri);
                http.Call();
            }
        }
    }
}
