using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DebtPayButton : MonoBehaviour
{
    public float DebtProportion = 0.1f;
    public float BankProportion = 0.25f;
    public int MinPayment = 10;
    public int Cost;
    public TextMeshProUGUI ButtonLabel;

    public void Pay()
    {
        DataStore.BankedMoney -= Cost;
        DataStore.RemainingMobDebt -= Cost;
    }

    void Update()
    {
        //pay remaining debt, proportion of debt, or proportion of bank (whichever is smallest)
        Cost = Mathf.FloorToInt(Mathf.Min(DataStore.RemainingMobDebt*DebtProportion < MinPayment ? DataStore.RemainingMobDebt : DataStore.RemainingMobDebt*DebtProportion, DataStore.BankedMoney*BankProportion));
        ButtonLabel.text = $"${Cost}";
        GetComponent<Button>().interactable = DataStore.BankedMoney >= Cost;
    }
}
