using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class LevelRequirements : MonoBehaviour
{
    public static LevelRequirements Singleton;
    public Inventory RequiredDrug {get; private set;}
    public int RequiredMoney => MobCut - DataStore.BankedMoney;
    public int MobCut {get; private set;} = 200;
    public Item[] PossibleRequiredDrugs;
    public float[] PossibleRequiredDrugsWeights;
    private float totalRequiredDrugsWeight;
    void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        foreach (var w in PossibleRequiredDrugsWeights)
        {
            totalRequiredDrugsWeight += w;
        }
        RequiredDrug = GetComponent<Inventory>();
        RequiredDrug.PickupItem(PickRequiredDrug());
    }

    public Item PickRequiredDrug()
    {
        return PossibleRequiredDrugs[Util.ChooseFromWeightedArray(PossibleRequiredDrugsWeights, totalRequiredDrugsWeight)];
    }

    public void SetLevelRequiredMoney(int money) {
        MobCut = money;
        DataStore.mobMoney = money;
    }

    public void ItemRecieved(Item item)
    {
        Debug.Log($"Testing requirement: {item.name}");
        if (item == null) return;
        if (item == RequiredDrug.Item) {
            RequiredDrug.DropItem();
            Debug.Log($"Requirement satisfied");
        }
        DataStore.BankedMoney += item.CashValue;
        DataStore.itemsSold.Add(item);
    }

    public bool Satisfied() => true;
}
