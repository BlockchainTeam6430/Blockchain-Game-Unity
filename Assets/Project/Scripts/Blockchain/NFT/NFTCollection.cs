using Newtonsoft.Json;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.Blockchain.NFT
{
    public class NFTCollection : EEReadyBehaviour
    {
        public string Chain = "ethereum";
        public string Network = "mainnet";
        public string Account;
        public string Contract;
        
        public NFTData[] Collection;
        
        public async void Get()
        {
            var response = await EVM.AllErc721(Chain, Network, Account, Contract);
            try
            {
                Collection = JsonConvert.DeserializeObject<NFTData[]>(response);
                Ready();
            }
            catch
            {
                EEException.Call(this, "Error: " + response);
            }
        }

        public void ChangeAccount(string account)
        {
            Account = account;
        }
        
        public void ChangeContract(string contract)
        {
            Contract = contract;
        }
    }
}