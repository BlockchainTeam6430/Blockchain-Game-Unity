using System.Collections.Generic;

namespace Project.Scripts.Network.Lobby
{
    public class SubserverData
    {
        public int MaxPlayers;
        public string URL;
        public List<string> PasswordsTemp = new List<string>();
        public string PasswordVerification;
        public string NFTURL;
        public string Wallet;
    }
}