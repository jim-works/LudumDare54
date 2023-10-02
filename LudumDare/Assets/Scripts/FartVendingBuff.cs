using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FartVendingBuff", order = 1)]
public class FartVendingBuff : VendingBuff
{
    public GameObject BuffPrefab;

    public override void OnFirstApplied(GameObject to) { 
        var go = Instantiate(BuffPrefab, to.transform.position, Quaternion.identity);
        go.transform.SetParent(to.transform);
        Debug.Log("asdflka" + go.name);
    }
    public override void OnStackIncreased(GameObject to, int stack) {
        FartBuff.Singleton.Stacks = stack;
    }
    public override void OnCleared(GameObject from)
    {
        Destroy(FartBuff.Singleton.gameObject);
    }
}