using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField]
    public Item Item {get; private set;}

    public Item PickupItem(Item pickup) {
        Item old = Item;
        if (Item != null) {
            DropItem();
        }
        Item = pickup;
        if (pickup != null) pickup.OnPickup(gameObject);
        return old;
    }
    public Item DropItem() {
        if (Item != null) Item.OnDrop(gameObject);
        Item i = Item;
        Item = null;
        return i;
    }
    public void SwapInventory(Inventory other) {
        Item pickup = other.DropItem();
        Item self = DropItem();
        other.PickupItem(self);
        PickupItem(pickup);
    }

}
