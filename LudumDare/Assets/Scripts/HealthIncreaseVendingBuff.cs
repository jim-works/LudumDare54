using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HealthIncreaseVendingBuff", order = 1)]
public class HealthIncreaseVendingBuff : VendingBuff
{
    public override void OnFirstApplied(GameObject to) { 
        to.GetComponent<Player>().Health++;
    }
    public override void OnStackIncreased(GameObject to, int stack) {
        to.GetComponent<Player>().Health++;
    }
}