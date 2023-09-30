using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Item item;

    public Item PickupItem(Item pickup) {
        Item old = item;
        if (item != null) {
            DropItem(item);
        }
        item = pickup;
        pickup.Holder = gameObject;
        pickup.OnPickup();
        return old;
    }
    public Item DropItem(Item drop) {
        drop.OnDrop();
        drop.Holder = null;
        return drop;
    }

}
