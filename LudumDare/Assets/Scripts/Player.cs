using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Player : MonoBehaviour
{
    public float PickpocketRadius = 3;
    public int Cash;
    public PickpocketUI PickpocketUI;

    private Inventory inv;
    void Start()
    {
        inv = GetComponent<Inventory>();
    }
    // Update is called once per frame
    void Update()
    {
        GameObject civ = ObjectRegistry.Singleton.GetClosestCivilian(transform.position, PickpocketRadius);
        PickpocketUI.gameObject.SetActive(civ != null);
        if (civ != null) {
            PickpocketUI.DisplayFor(civ.GetComponent<Inventory>(), 10);
        }
        
    }

    public void OnPickpocket(Inventory target, PickPocketResult result) {
        Debug.Log("Pickpocketed! " + result.ToString());
        if (result == PickPocketResult.Success) {
            inv.SwapInventory(target);
        }
    }
}
