using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemReceiver : MonoBehaviour
{
    public List<Item> ItemsRecieved {get; private set;} = new();
    void Start()
    {
        ObjectRegistry.Singleton.ItemReceiver = this;
    }
    public void RecieveItem(Item item) {
        if (item == null) return;
        ItemsRecieved.Add(item);
        LevelRequirements.Singleton.ItemRecieved(item);
        Debug.Log($"Recv item {item.name}, total: {CalculateTotal()}");
    }
    public int CalculateTotal() 
    {
        int total = 0;
        foreach (var item in ItemsRecieved) {
            total += item.CashValue;
        }
        return total;
    }

}
