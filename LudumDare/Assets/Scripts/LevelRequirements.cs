using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class LevelRequirements : MonoBehaviour
{
    public static LevelRequirements Singleton;
    public Inventory RequiredDrug {get; private set;}
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
}
