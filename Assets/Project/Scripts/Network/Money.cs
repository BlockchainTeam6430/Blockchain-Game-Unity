using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using TMPro;
using UnityEngine;

public class Money : EEBehaviourLoop
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private bool isGetAmount;
    
    [DllImport("__Internal")]
    private static extern void EEAddMoney(float amount);
        
    [DllImport("__Internal")]
    private static extern void EEWithdrawMoney(float amount);
    
    [DllImport("__Internal")]
    private static extern float EEGetAmountOfMoney();

    
    public void Add()
    {
        EEAddMoney(float.Parse(inputField.text));
    }
    public void Withdraw()

    {
        EEWithdrawMoney(float.Parse(inputField.text));
    }

    protected override void EEAwake()
    {
        base.EEAwake();
        Timing.Instance.TimeBetweenSlowUpdateCalls = 1f;
    }

    protected override void Loop()
    {
        base.Loop();
        if (!isGetAmount) return;
#if UNITY_WEBGL
         GetSelf<EEText>().SetData(EEGetAmountOfMoney().ToString(CultureInfo.InvariantCulture));
#endif
    }
}
