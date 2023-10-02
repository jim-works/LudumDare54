using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SusDecreaseVendingBuff", order = 1)]
public class SusDecreaseVendingBuff : VendingBuff
{
    public override void OnFirstApplied(GameObject to) { 
        Alarm.DecreaseAlarm(1);
    }
    public override void OnStackIncreased(GameObject to, int stack) {
        Alarm.DecreaseAlarm(1);
    }
}