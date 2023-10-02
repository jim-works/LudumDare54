using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseUpgrade : MonoBehaviour
{
    public StoreButton MidasButton;
    public StoreButton GymButton;
    public StoreButton SockButton;
    public DebtPayButton DebutButton;
    public int BaseMidasCost = 50;
    public int BaseGymCost = 25;
    public int BaseSockCost = 25;
    public float CostMultipler = 1.5f;
    void Start()
    {
        UpdateCosts();
    }
    public void UpdateCosts()
    {
        MidasButton.Cost = Mathf.CeilToInt(BaseMidasCost * Mathf.Pow(CostMultipler,DataStore.Midases));
        MidasButton.Count = DataStore.Midases;
        GymButton.Cost = Mathf.CeilToInt(BaseGymCost * Mathf.Pow(CostMultipler,DataStore.GymMemberships));
        GymButton.Count = DataStore.GymMemberships;
        SockButton.Cost = Mathf.CeilToInt(BaseSockCost * Mathf.Pow(CostMultipler,DataStore.Socks));
        SockButton.Count = DataStore.Socks;
    }
    public void PurchaseMidas()
    {
        DataStore.Midases++;
        DataStore.BankedMoney -= MidasButton.Cost;
        UpdateCosts();
    }
    public void PurchaseGymMembership()
    {
        DataStore.GymMemberships++;
        DataStore.BankedMoney -= GymButton.Cost;
        UpdateCosts();
    }
    public void PurchaseSock()
    {
        DataStore.Socks++;
        DataStore.BankedMoney -= SockButton.Cost;
        UpdateCosts();
    }
}
