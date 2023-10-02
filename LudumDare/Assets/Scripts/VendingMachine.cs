using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public VendingBuff[] Buffs;
    public float[] VendingBuffWeights;
    private float sumWeights;
    public int PurchasePrice = 100;
    void Start()
    {
        ObjectRegistry.Singleton.VendingMachines.Add(this);
        foreach (var w in VendingBuffWeights)
        {
            sumWeights += w;
        }
    }

    void OnDisable()
    {
        ObjectRegistry.Singleton.VendingMachines.Remove(this);
    }

    //triggers buff event if purchased
    public bool TryPurchase(GameObject purchaser, out VendingBuff buff)
    {
        if (DataStore.BankedMoney < PurchasePrice)
        {
            buff= null;
            return false;
        }
        DataStore.BankedMoney -= PurchasePrice;
        buff = Buffs[Util.ChooseFromWeightedArray(VendingBuffWeights, sumWeights)];
        buff.Apply(purchaser.GetComponent<Player>());
        return true;
    }
}
