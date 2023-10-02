using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class VendingMachineUI : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public UnityEvent<VendingBuff> OnPurchase;
    public float PurchaseCooldown = 0.5f;
    private VendingMachine displayingFor;
    private float lastTimeBought = -1;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTimeBought > PurchaseCooldown)
        {
            Text.text = displayingFor.PurchasePrice > DataStore.BankedMoney ? $"Insufficient funds: Need ${displayingFor.PurchasePrice} banked." : "Press [E] purchase a snack.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (displayingFor.TryPurchase(ObjectRegistry.Singleton.Player.gameObject, out VendingBuff b))
                {
                    OnPurchase.Invoke(b);
                    lastTimeBought = Time.time;
                }
            }
        } else {
            Text.text = "";
        }
    }

    public void DisplayFor(VendingMachine v)
    {
        if (displayingFor == null) displayingFor = v;
    }
}
