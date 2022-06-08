using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialContract : EEBehaviour
{
    // Start is called before the first frame update
    public static string abi = @"[{""inputs"": [{
        ""internalType"": ""address"",
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""name"": ""balances"",
      ""outputs"": [
        {
        ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [],
      ""name"": ""curRoom"",
      ""outputs"": [
        {
        ""internalType"": ""address"",
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""address"",
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""name"": ""gameLists"",
      ""outputs"": [
        {
        ""internalType"": ""address"",
          ""name"": ""first"",
          ""type"": ""address""
        },
        {
        ""internalType"": ""address"",
          ""name"": ""second"",
          ""type"": ""address""
        },
        {
        ""internalType"": ""bool"",
          ""name"": ""status"",
          ""type"": ""bool""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""address"",
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""name"": ""keyMaps"",
      ""outputs"": [
        {
        ""internalType"": ""string"",
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [],
      ""name"": ""owner"",
      ""outputs"": [
        {
        ""internalType"": ""address"",
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [],
      ""name"": ""roomprice"",
      ""outputs"": [
        {
        ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""string"",
          ""name"": ""a"",
          ""type"": ""string""
        },
        {
        ""internalType"": ""string"",
          ""name"": ""b"",
          ""type"": ""string""
        }
      ],
      ""name"": ""compareStringsbyBytes"",
      ""outputs"": [
        {
        ""internalType"": ""bool"",
          ""name"": """",
          ""type"": ""bool""
        }
      ],
      ""stateMutability"": ""pure"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""address"",
          ""name"": ""_address"",
          ""type"": ""address""
        },
        {
        ""internalType"": ""string"",
          ""name"": ""_key"",
          ""type"": ""string""
        }
      ],
      ""name"": ""checkUser"",
      ""outputs"": [
        {
        ""internalType"": ""bool"",
          ""name"": """",
          ""type"": ""bool""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""string"",
          ""name"": ""_key"",
          ""type"": ""string""
        }
      ],
      ""name"": ""postKey"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
    ""inputs"": [],
      ""name"": ""checkIfPosted"",
      ""outputs"": [
        {
        ""internalType"": ""bool"",
          ""name"": """",
          ""type"": ""bool""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
    ""inputs"": [],
      ""name"": ""addFund"",
      ""outputs"": [],
      ""stateMutability"": ""payable"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""uint256"",
          ""name"": ""_amount"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""withdraw"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
    ""inputs"": [],
      ""name"": ""createRoom"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""address"",
          ""name"": ""_room"",
          ""type"": ""address""
        }
      ],
      ""name"": ""joinRoom"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
    ""inputs"": [
        {
        ""internalType"": ""address"",
          ""name"": ""_room"",
          ""type"": ""address""
        },
        {
        ""internalType"": ""address"",
          ""name"": ""_winner"",
          ""type"": ""address""
        }
      ],
      ""name"": ""gameOver"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
    ""inputs"": [],
      ""name"": ""claim"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    }
  ]";
    public static string contract = "0x25bEa288c4DFD00949E0Fc0951539163B24564d1";
    public string usdt = "";
    public static InitialContract Instance;
    public static double amount = 0.0501;
    public static float decimals = 1000000000000000000;
    public static string chain = "4";
    public static string network = "Rinkeby";
    protected override void EEAwake()
    {
        base.EEAwake();
        Instance = this;
    }

}
