namespace Plugins.EntenEller.Base.Scripts.Blockchain.NFT
{
    public class NFTCollectionMyWallet : NFTCollection
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            ChangeAccount(Wallet.Account);
        }
    }
}
