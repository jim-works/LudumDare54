using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DataStoreUI : MonoBehaviour
{
    public TextMeshProUGUI BankedText;
    public TextMeshProUGUI DebtText;
    public TextMeshProUGUI LastRunTimeText;
    public TextMeshProUGUI TotalTimeText;
    public TextMeshProUGUI RunCountText;
    void Update()
    {
        BankedText.text = $"Banked: ${DataStore.BankedMoney}";
        DebtText.text = $"Debt: ${DataStore.RemainingMobDebt}";
        LastRunTimeText.text = $"Last Run: {DataStore.LastRunTime:0.0}s";
        TotalTimeText.text = $"Total: {DataStore.ToalRunTime:0.0}s";
        RunCountText.text = $"Runs: {DataStore.RunCount}";
    }
}
