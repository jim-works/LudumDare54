using UnityEngine;

public class VendingBuff : ScriptableObject
{
    public string Description;
    public void Apply(Player to) {
        to.AddBuff(this);
    }

    public virtual void OnFirstApplied(GameObject to) {}
    public virtual void OnStackIncreased(GameObject to, int currStack) {}
    public virtual void OnCleared(GameObject from) {}
}