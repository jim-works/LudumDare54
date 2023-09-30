using UnityEngine;

public class Item {
    public GameObject Holder;
    public virtual void OnPickup() {}
    public virtual void OnDrop() {}
    public virtual void OnUse() {}
}