using Plugins.EntenEller.Base.Scripts.Blockchain;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;





public class ContractController : MonoBehaviour
{

    public TextMeshProUGUI walletBalance;

    // called third
    void Start()
    {
        Debug.Log("---Start---");

        Balance();
    }

    public async void AddFund()
    {
        string fundValue = GameObject.Find("FundAmount").GetComponent<TMP_InputField>().text;
        float decimals = 1000000000000000000; // 18 decimals 

        float fundAmount = float.Parse(fundValue);
        if (fundAmount < 0)
        {
            return;
        }

        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return;
        }
        Debug.Log(Wallet.Account);

        // account to send to
        // value in wei
        string value = Convert.ToDecimal(fundAmount * decimals).ToString();

        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
     #if UNITY_WEBGL
        try
        {
            string response = await Web3GL.SendContract("addFund", InitialContract.abi, InitialContract.contract, "[]", value, gasLimit, gasPrice);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
      
     #else
        
        string data = await EVM.CreateContractData(InitialContract.abi, "addFund", "");
        string response = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, value, data, gasLimit, gasPrice); ;
     #endif

    }

    public async void WithrawWallet()
    {
        string withdrawValue = GameObject.Find("WithdrawAmount").GetComponent<TMP_InputField>().text;
        float decimals = 1000000000000000000; // 18 decimals 

        float withdrawAmount = float.Parse(withdrawValue);
        if (withdrawAmount < 0)
        {
            return;
        }

        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return;
        }
        Debug.Log(Wallet.Account);

        // account to send to
        // value in wei
        string args = "[\"" + Convert.ToDecimal(withdrawAmount * decimals).ToString() + "\"]";

        // data OPTIONAL

        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
     #if UNITY_WEBGL
        try
        {
            string response = await Web3GL.SendContract("withdraw", InitialContract.abi, InitialContract.contract, args, "0", gasLimit, gasPrice);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
     #else
        
        string data = await EVM.CreateContractData(InitialContract.abi, "withdraw", "");
        string response = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, "0", data, gasLimit, gasPrice); ;
     #endif
        Debug.Log("WithdrawPart");
    }



    public static async void CreateRoom()
    {
        
        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return;
        }
        Debug.Log(Wallet.Account);

        // account to send to
        // value in wei
        string args = "[]";

        // data OPTIONAL

        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
     #if UNITY_WEBGL
        try
        {
            string response = await Web3GL.SendContract("createRoom", InitialContract.abi, InitialContract.contract, args, "0", gasLimit, gasPrice);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
     #else
        
        string data = await EVM.CreateContractData(InitialContract.abi, "createRoom", "");
        string response = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, "0", data, gasLimit, gasPrice); ;
     #endif
        Debug.Log("createRoom");
    }

    public static async Task<string> CurrentRoom()
    {
        string curRoom = "";
        
        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return curRoom;
        }
        // value in wei
        string args = "[]";

        // data OPTIONAL

        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
     #if UNITY_WEBGL
        try
        {
            curRoom = await Web3GL.SendContract("curRoom", InitialContract.abi, InitialContract.contract, args, "0", gasLimit, gasPrice);
            Debug.Log(curRoom);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
     #else

        string data = await EVM.CreateContractData(InitialContract.abi, "curRoom", "");
        curRoom = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, "0", data, gasLimit, gasPrice); ;
     #endif
        Debug.Log("createRoom");

        return curRoom;
    }

    public static async void JoinRoom(string roomAddr)
    {

        if (roomAddr.Length ==  0)
        {
            return;
        }

        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return;
        }
        Debug.Log(Wallet.Account);

        // account to send to

        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
     #if UNITY_WEBGL
        Debug.Log(InitialContract.contract);
        try
        {
            string response = await Web3GL.SendContract("joinRoom", InitialContract.abi, InitialContract.contract, roomAddr, "0", gasLimit, gasPrice);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
      
     #else
        
        string data = await EVM.CreateContractData(InitialContract.abi, "joinRoom", roomAddr);
        string response = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, "0", data, gasLimit, gasPrice); ;
     #endif

    }

    public async void Login()
    {
        string password = GameObject.Find("LgPassword").GetComponent<TMP_InputField>().text;

        if (password.Length == 0)
        {
            return;
        }

        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return;
        }
        Debug.Log(Wallet.Account);

        // account to send to

        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
     #if UNITY_WEBGL
        Debug.Log(InitialContract.contract);
        try
        {
            string response = await Web3GL.SendContract("checkUser", InitialContract.abi, InitialContract.contract, $"[{Wallet.Account}, {password}]", "0", gasLimit, gasPrice);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
      
     #else
        
        string data = await EVM.CreateContractData(InitialContract.abi, "checkUser", $"[{Wallet.Account},{password}]");
        string response = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, "0", data, gasLimit, gasPrice); ;
     #endif

    }

    public async void Register()
    {
        string password = GameObject.Find("RgPassword").GetComponent<TMP_InputField>().text;

        if (password.Length ==  0)
        {
            return;
        }

        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return;
        }
        Debug.Log(Wallet.Account);

        // account to send to

        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
     #if UNITY_WEBGL
        Debug.Log(InitialContract.contract);
        try
        {
            string response = await Web3GL.SendContract("postKey", InitialContract.abi, InitialContract.contract, password, "0", gasLimit, gasPrice);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
      
     #else
        
        string data = await EVM.CreateContractData(InitialContract.abi, "postKey", password);
        string response = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, "0", data, gasLimit, gasPrice); ;
     #endif

    }

    public async void Balance() {
        
        walletBalance.text = "22";
        Debug.Log("-------------" + Wallet.Account);
        
        if (Wallet.IsConnected == false || Wallet.Account == null)
        {
            return;
        }
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        // send transaction
#if UNITY_WEBGL
        Debug.Log(InitialContract.contract);
        try
        {
            string response = await Web3GL.SendContract("balances", InitialContract.abi, InitialContract.contract, $"[{Wallet.Account}]", "0", gasLimit, gasPrice);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
      
#else

        string data = await EVM.CreateContractData(InitialContract.abi, "balances", "[" + PlayerPrefs.GetString("Account") + "]");
        string response = await Web3Wallet.SendTransaction(InitialContract.chain, InitialContract.contract, "0", data, gasLimit, gasPrice); ;

        walletBalance.text = response;
     #endif
    }

}
