using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnItemScriptableObject", order = 1)]
public class Item : ScriptableObject {
    public Sprite Icon;
    public int CashValue;
    public virtual void OnPickup(GameObject holder) {}
    public virtual void OnDrop(GameObject holder) {}
    public virtual void OnUse(GameObject holder) {}
}