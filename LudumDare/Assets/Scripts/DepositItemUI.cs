using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositItemUI : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ObjectRegistry.Singleton.GetClosestItemReciever(Vector3.zero, float.PositiveInfinity).RecieveItem(ObjectRegistry.Singleton.Player.Inventory.DropItem());
        }
    }
}
